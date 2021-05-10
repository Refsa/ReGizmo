#include "UnityCG.cginc"
#include "ReGizmoShaderUtils.cginc"
#include "SDF_Utils.cginc"

struct Data {
    float3 position;
    float3 normal;
    float radius;
    float thickness;
    float3 color;
};
StructuredBuffer<Data> _Properties;

struct v2g_2d
{
    uint vertexID: TEXCOORD0;
};

struct g2f_2d
{
    float4 pos: SV_POSITION;
    float2 uv: TEXCOORD0;
    nointerpolation float3 color: TEXCOORD1;
    nointerpolation float inner_radius: TEXCOORD2;
};

v2g_2d vert_2d (uint vertexID : SV_VertexID)
{
    v2g_2d o;
    o.vertexID = vertexID;
    return o;
}

float3x3 angle_axis(float angle, float3 axis)
{
    float c, s;
    sincos(angle, s, c);

    float t = 1 - c;
    float x = axis.x;
    float y = axis.y;
    float z = axis.z;

    return float3x3(
        t * x * x + c,      t * x * y - s * z,  t * x * z + s * y,
        t * x * y + s * z,  t * y * y + c,      t * y * z - s * x,
        t * x * z - s * y,  t * y * z + s * x,  t * z * z + c
    );
}

float cos_sim(float3 a, float3 b)
{
    float d = dot(a, b);
    float l = dot(a, a) * dot(b, b);
    float s = clamp(d / l, -1, 1);

    return d / l;
}

[maxvertexcount(4)]
void geom_2d(point v2g_2d i[1], inout TriangleStream<g2f_2d> triangleStream)
{
    Data bd = _Properties[i[0].vertexID];

    float4 clip = UnityObjectToClipPos(float4(bd.position, 1.0));
    float4 cp1, cp2, cp3, cp4;
    float inner_radius = (bd.thickness / bd.radius) / _ScreenParams.x * clip.w;

    float norm_len = dot(bd.normal, bd.normal);
    
    if (norm_len > 1.1)
    {
        float3 normal = normalize(bd.normal);

        float3 leftright = float3(0,1,0), updown = float3(0,0,1);
        leftright = normalize(cross(leftright, normal));
        updown = normalize(cross(normal, leftright)) * bd.radius;
        leftright *= bd.radius;

        float3 top_left = bd.position - updown - leftright;
        float3 top_right = bd.position - updown + leftright;
        float3 bottom_right = bd.position + updown + leftright;
        float3 bottom_left = bd.position + updown - leftright;

        cp1 = UnityObjectToClipPos(float4(top_left, 1.0));
        cp2 = UnityObjectToClipPos(float4(top_right, 1.0));
        cp4 = UnityObjectToClipPos(float4(bottom_left, 1.0));
        cp3 = UnityObjectToClipPos(float4(bottom_right, 1.0));
    }
    else if (norm_len != 0.0)
    {
        float3 normal = normalize(bd.normal);
        float3 view_dir = normalize(ObjSpaceViewDir(float4(bd.position, 1.0)));

        float3 leftright = normal;
        float a = dot(normal, view_dir);

        float3 updown = UNITY_MATRIX_IT_MV[1].xyz;
        if (abs(a) < 1.0)
        {
            updown = cross(normal, view_dir);
        }
        updown = normalize(updown);

        updown *= bd.radius;
        leftright *= bd.radius;

        float3 top_left = bd.position - updown - leftright;
        float3 top_right = bd.position - updown + leftright;
        float3 bottom_right = bd.position + updown + leftright;
        float3 bottom_left = bd.position + updown - leftright;

        cp1 = UnityObjectToClipPos(float4(top_left, 1.0));
        cp2 = UnityObjectToClipPos(float4(top_right, 1.0));
        cp4 = UnityObjectToClipPos(float4(bottom_left, 1.0));
        cp3 = UnityObjectToClipPos(float4(bottom_right, 1.0));
    }
    else
    {
        float halfOffset = bd.radius;
        float2 size = float2(-halfOffset, -halfOffset);

        // Scale the size to screen coords
        size /= _ScreenParams.xy;
        size *= clip.w;

        if (ProjectionFlipped())
        {
            size.y = -size.y;
        }

        // Create billboard vertices
        cp1 = float4(clip.x - size.x, clip.y - size.y, clip.zw);
        cp2 = float4(clip.x - size.x, clip.y + size.y, clip.zw);
        cp3 = float4(clip.x + size.x, clip.y + size.y, clip.zw);
        cp4 = float4(clip.x + size.x, clip.y - size.y, clip.zw);
    }


    g2f_2d g1;
    g1.pos = cp1;
    g1.uv = float2(1.02, 0);
    g1.color = bd.color;
    g1.inner_radius = inner_radius;

    g2f_2d g2;
    g2.pos = cp2;
    g2.uv = float2(1.02, 1.02);
    g2.color = bd.color;
    g2.inner_radius = inner_radius;

    g2f_2d g3;
    g3.pos = cp3;
    g3.uv = float2(0, 1.02);
    g3.color = bd.color;
    g3.inner_radius = inner_radius;

    g2f_2d g4;
    g4.pos = cp4;
    g4.uv = float2(0, 0);
    g4.color = bd.color;
    g4.inner_radius = inner_radius;

    triangleStream.Append(g2);
    triangleStream.Append(g1);
    triangleStream.Append(g3);
    triangleStream.Append(g4);
    triangleStream.RestartStrip();
}