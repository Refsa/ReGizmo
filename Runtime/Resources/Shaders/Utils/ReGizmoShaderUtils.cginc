#include "UnityCG.cginc"

struct DMIIProperties {
    float3 Position;
    float3 Rotation;
    float3 Scale;
    float4 Color;
};


float4 Rotate3D(float3 pos, float3 rotation)
{
    float cosX = cos(rotation.x);
    float sinX = sin(rotation.x);
    float cosY = cos(rotation.y);
    float sinY = sin(rotation.y);
    float cosZ = cos(rotation.z);
    float sinZ = sin(rotation.z);

    float4x4 rotX =
        float4x4(
            1, 0, 0, 0,
            0, cosX, -sinX, 0,
            0, sinX, cosX, 0,
            0, 0, 0, 1
        );

    float4x4 rotY =
        float4x4(
            cosY, 0, sinY, 0,
            0, 1, 0, 0,
            -sinY, 0, cosY, 0,
            0, 0, 0, 1
        );

    float4x4 rotZ =
        float4x4(
            cosZ, -sinZ, 0, 0,
            sinZ, cosZ, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1
        );

    float4x4 rotMat = mul(mul(rotX, rotY), rotZ);

    return mul(rotMat, pos);
}

float4 TRS(float3 position, float3 rotation, float3 scale, float4 vLoc)
{
    float4x4 posMat =
        float4x4(
            1, 0, 0, position.x,
            0, 1, 0, position.y,
            0, 0, 1, position.z,
            0, 0, 0, 1
        );
    
    float4x4 scaleMat =
        float4x4(
            scale.x, 0, 0, 0,
            0, scale.y, 0, 0,
            0, 0, scale.z, 0,
            0, 0, 0, 1
        );

    float cosX = cos(rotation.x);
    float sinX = sin(rotation.x);
    float cosY = cos(rotation.y);
    float sinY = sin(rotation.y);
    float cosZ = cos(rotation.z);
    float sinZ = sin(rotation.z);

    float4x4 rotX =
        float4x4(
            1, 0, 0, 0,
            0, cosX, -sinX, 0,
            0, sinX, cosX, 0,
            0, 0, 0, 1
        );

    float4x4 rotY =
        float4x4(
            cosY, 0, sinY, 0,
            0, 1, 0, 0,
            -sinY, 0, cosY, 0,
            0, 0, 0, 1
        );

    float4x4 rotZ =
        float4x4(
            cosZ, -sinZ, 0, 0,
            sinZ, cosZ, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1
        );

    float4x4 rotMat = mul(mul(rotZ, rotY), rotX);

    float4 pos = vLoc;
    pos = mul(scaleMat, pos);
    pos = mul(rotMat, pos);
    pos = mul(posMat, pos);

    return pos;
}

// https://forum.unity.com/threads/incorrect-normals-on-after-rotating-instances-graphics-drawmeshinstancedindirect.503232/#post-3277479
float4x4 inverse(float4x4 input)
{
#define minor(a,b,c) determinant(float3x3(input.a, input.b, input.c))
 
    float4x4 cofactors = float4x4(
        minor(_22_23_24, _32_33_34, _42_43_44),
        -minor(_21_23_24, _31_33_34, _41_43_44),
        minor(_21_22_24, _31_32_34, _41_42_44),
        -minor(_21_22_23, _31_32_33, _41_42_43),
 
        -minor(_12_13_14, _32_33_34, _42_43_44),
        minor(_11_13_14, _31_33_34, _41_43_44),
        -minor(_11_12_14, _31_32_34, _41_42_44),
        minor(_11_12_13, _31_32_33, _41_42_43),
 
        minor(_12_13_14, _22_23_24, _42_43_44),
        -minor(_11_13_14, _21_23_24, _41_43_44),
        minor(_11_12_14, _21_22_24, _41_42_44),
        -minor(_11_12_13, _21_22_23, _41_42_43),
 
        -minor(_12_13_14, _22_23_24, _32_33_34),
        minor(_11_13_14, _21_23_24, _31_33_34),
        -minor(_11_12_14, _21_22_24, _31_32_34),
        minor(_11_12_13, _21_22_23, _31_32_33)
        );
#undef minor
    return transpose(cofactors) / determinant(input);
}

float ConstantWidth(float camDist, float invCenterDist, float distance)
{
    float camDistScale = 1 - clamp(camDist / distance, 0, 1);
    float a = exp2(-2.7 * invCenterDist * 3) * camDistScale;

    return a;
}

float DistanceFade(float camDist, float distance)
{
    float f = 1 - (camDist / distance);
    return f;
}

float CamDistSampleFactorLine(float3 pos, float3 cameraPos)
{
    return distance(cameraPos, pos) * 0.2;
    // return distance(cameraPos, pos) * lineWidth;
}

float SincFilter(float2 uv, float radius)
{
    return 
    radius * 
        float2(
            sin(uv.x) / uv.x,
            sin(uv.y) / uv.y
        );
}

float PixelVariance(float2 uv, float sigma2, out float2 ddu, out float2 ddv)
{
    ddu = ddx(uv);
    ddv = ddy(uv);
    return sigma2 * (dot(ddu, ddu) + dot(ddv, ddv));
} 

float AreaPixelVariance(float2 uv)
{
    float2 ddu, ddv, ddtu, ddtv = 0;
    float ssLength = dot(_ScreenParams.zw, _ScreenParams.zw);

    float variance = PixelVariance(uv, 0.05, ddu, ddv);

    float areaSampleUV = 0;
    areaSampleUV += PixelVariance(uv + ddu, 0.25, ddtu, ddtv);
    areaSampleUV += PixelVariance(uv - ddu, 0.25, ddtu, ddtv);
    areaSampleUV += PixelVariance(uv + ddv, 0.25, ddtu, ddtv);
    areaSampleUV += PixelVariance(uv - ddv, 0.25, ddtu, ddtv);
    areaSampleUV /= 4;

    // float areaSampleLine = 0;
    // areaSampleLine += PixelVariance(uv + lineDir * ssLength * -2, 0.75, ddtu, ddtv);
    // areaSampleLine += PixelVariance(uv + lineDir * ssLength * -1, 0.75, ddtu, ddtv);
    // areaSampleLine += PixelVariance(uv + lineDir * ssLength * 1, 0.75, ddtu, ddtv);
    // areaSampleLine += PixelVariance(uv + lineDir * ssLength * 2, 0.75, ddtu, ddtv);
    // areaSampleLine /= 4;

    return areaSampleUV + variance;
}

float SampleLineDist(float2 uv, float widthScale)
{
    const float2 centeruv = float2(0.5, uv.y);
    return 1.0 - distance(centeruv, uv);
}

float GetDepthScalingFactor(float4 projectedPos)
{
    return 1.0 / projectedPos.w;
}

float GetScreenSpacePos(float4 clipPos)
{
    return _ScreenParams.xy * (0.5 * clipPos.xy / clipPos.w + 0.5);
}

float3 Posterize(float3 val, float3 steps)
{
    return floor(val / (1 / steps)) * (1 / steps);
}

float3 BlinnPhong(float3 lightDir, float3 viewDir, float3 normal, float3 specColor, float shinyness)
{
    float ndotl = dot(viewDir, lightDir);
    float intensity = saturate(ndotl);
    float3 halfDir = normalize(viewDir + lightDir);
    float specAngle = max(dot(halfDir, normal), 0);
    float specular = pow(specAngle, shinyness);
    float lambertian = max(dot(lightDir, normal), 0);

    return 
        float3(1,1,1) * specular * lambertian;
}

float3 ViewRefract(float3 viewDir, float3 lightDir, float3 normal)
{
    float3 halfDir = normalize(viewDir + lightDir);
    float3 viewReflect = reflect(viewDir, normal);
    float3 lightReflect = reflect(lightDir, normal);
    float3 surfaceRefract = -refract(halfDir, normal, dot(viewReflect, lightReflect));

    return surfaceRefract;
}