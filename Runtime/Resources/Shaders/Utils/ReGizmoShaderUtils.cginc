#include "UnityCG.cginc"
#include "ShaderProperties.cginc"

// ## MATH CONSTANTS ##
static const float E = 2.71828;
static const float HalfE = 2.71828 * 0.5;
static const float2 PixelSize = 1.0 / _ScreenParams.xy;
static const float PixelArea = length(PixelSize);

// ## FLAGS ##
static const int DRAW_MODE_BILLBOARD_FREE = 1 << 0;
static const int DRAW_MODE_BILLBOARD_ALIGNED = 1 << 1;
static const int DRAW_MODE_AXIS_ALIGNED = 1 << 2;

static const int SIZE_MODE_PIXEL = 1 << 11;
static const int SIZE_MODE_PERCENT = 1 << 12;
static const int SIZE_MODE_UNIT = 1 << 13;

static const int FILL_MODE_FILL = 1 << 20;
static const int FILL_MODE_OUTLINE = 1 << 21;

// Shared Data (cbuffer)
int _ZTest;

float4x4 rotation_matrix(float3 rotation)
{
    float cosX = cos(rotation.x), sinX = sin(rotation.x);
    float cosY = cos(rotation.y), sinY = sin(rotation.y);
    float cosZ = cos(rotation.z), sinZ = sin(rotation.z);

    return float4x4(
    cosZ * cosY, -sinZ * cosX + cosZ * sinY * sinX, sinZ * sinX + cosZ * sinY * cosX, 0,
    sinZ * cosY, cosZ * cosX + sinZ * sinY * sinX, -cosZ * sinX + sinZ * sinY * cosX, 0,
    -sinY, cosY * sinX, cosY * cosX, 0,
    0,0,0,1
    );
}

// https://jp.mathworks.com/help/aeroblks/quaternioninverse.html
float4 q_inverse(float4 q)
{
    float4 conj = float4(-q.x, -q.y, -q.z, q.w);
    return conj / (q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);
}

// http://mathworld.wolfram.com/Quaternion.html
float4 q_mul(float4 q1, float4 q2)
{
    return float4(
    q2.xyz * q1.w + q1.xyz * q2.w + cross(q1.xyz, q2.xyz),
    q1.w * q2.w - dot(q1.xyz, q2.xyz)
    );
}

// http://mathworld.wolfram.com/Quaternion.html
float3 rotate_vector(float4 quat, float3 vec)
{
    quat = q_inverse(quat);
    return vec + 2.0 * cross( cross( vec, quat.xyz ) + quat.w * vec, quat.xyz );
}

float4 rotate_angle_axis(float angle, float3 axis)
{
    float sn = sin(angle * 0.5);
    float cs = cos(angle * 0.5);
    return float4(axis * sn, cs);
}

float4 TRS(float3 position, float4 rotation, float3 scale, float4 vLoc)
{
    return float4(position + rotate_vector(rotation, vLoc.xyz * scale), 1.0);
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

bool has_flag(int mask, int flag)
{
    return (mask & flag) != 0;
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

bool ProjectionFlipped()
{
    #if UNITY_UV_STARTS_AT_TOP
        if (_ProjectionParams.x > 0)
        {
            return true;
        }
    #endif

    return false;
}

inline float compute_depth(float4 vpos)
{
    return -(UnityObjectToViewPos( vpos ).z * _ProjectionParams.w);
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

float Remap_float(float In, float2 InMinMax, float2 OutMinMax)
{
    return OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
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
    float3(1, 1, 1) * specular * lambertian;
}

float3 ViewRefract(float3 viewDir, float3 lightDir, float3 normal)
{
    float3 halfDir = normalize(viewDir + lightDir);
    float3 viewReflect = reflect(viewDir, normal);
    float3 lightReflect = reflect(lightDir, normal);
    float3 surfaceRefract = -refract(halfDir, normal, dot(viewReflect, lightReflect));

    return surfaceRefract;
}

// OIT
struct TrFrag
{
    float4 c1: COLOR1;
    float4 c2: COLOR2;
};


float computeWeight(float4 color, float z)
{
    float weight =
    max(min(1.0, max(max(color.r, color.g), color.b) * color.a), color.a) *
    clamp(0.03 / (0.00001 + pow(z / 200, 4.0)), 0.01, 3000);
    return weight;
}


TrFrag encodeTransparency(float4 col, float z)
{
    //	float weight = computeWeight(color, z); 

    float weight = computeWeight(col, z);
    float4 weightColor = float4(col.xyz, 1.0) * weight;
    float4 weightPremulColor = weightColor * col.w;
    float alpha = col.w;

    TrFrag res;

    res.c1 = float4(weightPremulColor.xyz, 1.0);
    res.c2 = 0.0;
    res.c2.yz = weightPremulColor.w;
    res.c2.w = alpha;

    return res;
}

// https://www.shadertoy.com/view/lljGWR
float lineWu(float2 a, float2 b, float2 c)
{   
    float minX = min(a.x, b.x);
    float maxX = max(a.x, b.x);
    float minY = min(a.y, b.y);
    float maxY = max(a.y, b.y);

    // crop line segment
    if (c.x < floor(minX) || c.x > ceil(maxX))
    return 0.0;
    if (c.y < floor(minY) || c.y > ceil(maxY))
    return 0.0;

    // swap X and Y
    float2 d = float2(abs(a.x - b.x), abs(a.y - b.y));
    if (d.y > d.x) 
    {
        a = a.yx;
        b = b.yx;
        c = c.yx;
        d = d.yx;

        minX = minY;
        maxX = maxY;
    } 

    // handle end points
    float k = 1.0;
    if (c.x == floor(minX))
    k = 1.0 - frac(minX);
    if (c.x == ceil(maxX))
    k = frac(maxX);


    // find Y by two-point form of linear equation
    // http://en.wikipedia.org/wiki/Linear_equation#Two-point_form
    float y = (b.y - a.y) / (b.x - a.x) * (c.x - a.x) + a.y;

    // calculate result brightness
    float br = 1.0 - abs(c.y - y);
    return max(0.0, br) * k * (0.5 * length(d) / d.x);
}

float GetDepthFromClip(float4 clipPos)
{
    float4 screenPos = ComputeScreenPos(clipPos);
    return UNITY_Z_0_FAR_FROM_CLIPSPACE(screenPos.z);
}

// Weighted-Blended OIT

float wb_oit(float z, float alpha) {
    return pow(z, -2.5);
    // return alpha * max(1e-2, min(3 * 1e3, 10.0/(1e-5 + pow(z/5, 2) + pow(z/200, 6))));
    // return alpha * max(1e-2, min(3 * 1e3, 0.03/(1e-5 + pow(z/200, 4))));
}