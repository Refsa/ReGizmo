#include "Utils/ReGizmoShaderUtils.cginc"

struct LineProperties
{
    float3 Position;
    float4 Color;
    float Width;
};

StructuredBuffer<LineProperties> _Properties;

struct v2g_line
{
    uint vertexID: TEXCOORD0;
};

struct g2f_line
{
    float4 pos: SV_POSITION;
    noperspective float2 uv : TEXCOORD0;
    float4 color: TEXCOORD1;
    nointerpolation float width: TEXCOORD2;
};

[maxvertexcount(6)]
void geom_line(line v2g_line i[2], inout TriangleStream<g2f_line> triangleStream)
{
    LineProperties prop1 = _Properties[i[0].vertexID];
    LineProperties prop2 = _Properties[i[1].vertexID];

    float4 p1 = UnityObjectToClipPos(float4(prop1.Position, 1.0));
    float4 p2 = UnityObjectToClipPos(float4(prop2.Position, 1.0));

    // Sort by point "closest" to screen-space
    if (p1.w > p2.w)
    {
        float4 pos_temp = p1;
        p1 = p2;
        p2 = pos_temp;

        LineProperties prop_temp = prop1;
        prop1 = prop2;
        prop2 = prop_temp;
    }
    
    // Manual near-clip
    if (p1.w < _ProjectionParams.y)
    {
        float ratio = (_ProjectionParams.y - p1.w) / (p2.w - p1.w);
        p1 = lerp(p1, p2, ratio);
    }

    float w1 = ceil(prop1.Width + PixelSize);
    float w2 = ceil(prop2.Width + PixelSize);

    float2 a = p1.xy / p1.w;
    float2 b = p2.xy / p2.w;
    float2 c1 = normalize(float2(a.y - b.y, b.x - a.x)) / _ScreenParams.xy * w1;
    float2 c2 = normalize(float2(a.y - b.y, b.x - a.x)) / _ScreenParams.xy * w2;

    g2f_line g0 = (g2f_line)0;
    g2f_line g1 = (g2f_line)0;
    g2f_line g2 = (g2f_line)0;
    g2f_line g3 = (g2f_line)0;

    g0.pos = float4(p1.xy + c1 * p1.w, p1.zw);
    g1.pos = float4(p1.xy - c1 * p1.w, p1.zw);
    g2.pos = float4(p2.xy + c2 * p2.w, p2.zw);
    g3.pos = float4(p2.xy - c2 * p2.w, p2.zw);

    g0.uv = float2(0, 0);
    g1.uv = float2(1, 0);
    g2.uv = float2(0, 1);
    g3.uv = float2(1, 1);

    g0.color = prop1.Color;
    g1.color = prop1.Color;
    g2.color = prop2.Color;
    g3.color = prop2.Color;

    g0.width = w1;
    g1.width = w1;
    g2.width = w2;
    g3.width = w2;

    if (ProjectionFlipped())
    {
        triangleStream.Append(g1);
        triangleStream.Append(g0);
        triangleStream.Append(g3);
        triangleStream.Append(g2);
    }
    else
    {
        triangleStream.Append(g0);
        triangleStream.Append(g1);
        triangleStream.Append(g2);
        triangleStream.Append(g3);
    }
    triangleStream.RestartStrip();
}

v2g_line vert_line(uint vertexID: SV_VertexID)
{
    v2g_line f = (v2g_line)0;
    f.vertexID = vertexID;
    return f;
}

float4 frag_line(g2f_line g) : SV_Target
{
    float4 col = g.color;

    // TODO: Unoptimal since we can avoid this distance check, but works for now
    const float2 center_uv = float2(0.5, g.uv.y);
    const float dist = distance(g.uv, center_uv) * 2;

    static const float smoothing = 0.3;
    static const float sharpness = 2;

    float x = pow(dist, g.width * smoothing);
    col.a = exp2(-2.7 * pow(x, sharpness));

    return col;
}