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

[maxvertexcount(4)]
void geom_2d(point v2g_2d i[1], inout TriangleStream<g2f_2d> triangleStream)
{
    Data bd = _Properties[i[0].vertexID];
    float4 clip = mul(UNITY_MATRIX_VP, float4(bd.position, 1.0));

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
    float4 cp1 = float4(clip.x - size.x, clip.y - size.y, clip.z, clip.w);
    float4 cp2 = float4(clip.x - size.x, clip.y + size.y, clip.z, clip.w);
    float4 cp3 = float4(clip.x + size.x, clip.y + size.y, clip.z, clip.w);
    float4 cp4 = float4(clip.x + size.x, clip.y - size.y, clip.z, clip.w);

    float inner_radius = (bd.thickness / bd.radius);

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