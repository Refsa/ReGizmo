#include "UnityCG.cginc"
#include "Utils/ReGizmoShaderUtils.cginc"

#ifndef DEFAULT_SCALE_FACTOR
#define DEFAULT_SCALE_FACTOR 0.142
#endif

struct font_v2g
{
    uint vertexID : TEXCOORD0;
};

struct font_g2f
{
    float4 pos: SV_POSITION;
    float2 uv: TEXCOORD0;
    float3 color: TEXCOORD1;
    float2 scale: TEXCOORD2;
};

struct CharacterInfo
{
    float2 BottomLeft;
    float2 BottomRight;
    float2 TopLeft;
    float2 TopRight;
    float4 Size;
    float Advance;
};

struct CharData
{
    uint TextID;
    uint CharIndex;
    float Advance;
};

struct TextData
{
    float3 Color;
    float Scale;
    float3 Position;
    float CenterOffset;
};

// INPUTS
StructuredBuffer<CharData> _CharData;
StructuredBuffer<CharacterInfo> _CharacterInfos;
StructuredBuffer<TextData> _TextData;

sampler2D _MainTex;
float4 _MainTex_ST;
float4 _MainTex_TexelSize;

// SDF INPUTS
float _DistanceRange;
float2 _AtlasDimensions;
float _AtlasSize;
static const float default_atlas_size = 128;
static const float atlas_size_factor = _AtlasSize / default_atlas_size;
const static float default_scale_factor_sqr = DEFAULT_SCALE_FACTOR * DEFAULT_SCALE_FACTOR;
const static float aspect_ratio = _ScreenParams.y / _ScreenParams.x;

// PROGRAMS
font_v2g font_vert(uint vid : SV_VertexID)
{
    font_v2g o;
    o.vertexID = vid;
    return o;
}

[maxvertexcount(4)]
void font_geom(point font_v2g i[1], inout TriangleStream<font_g2f> triangleStream)
{
    uint vid = i[0].vertexID;

    CharData cd = _CharData[vid];
    TextData td = _TextData[cd.TextID];
    CharacterInfo ci = _CharacterInfos[cd.CharIndex];

    // HACK: Move along, just another magic number because math below is wrong
    static const float scale_factor = 2.7;

    float4 centerClip = UnityObjectToClipPos(float4(td.Position, 1.0));
    float camDist = centerClip.w;

    float4 advanceOffset = float4(cd.Advance, 0, 0, 0) * scale_factor;
    float4 size = ci.Size * td.Scale * scale_factor;

    float4 c1 = (float4(size.x * aspect_ratio, -size.w, 0, 0) + advanceOffset) * camDist;
    float4 c2 = (float4(size.y * aspect_ratio, -size.w, 0, 0) + advanceOffset) * camDist;
    float4 c3 = (float4(size.y * aspect_ratio, -size.z, 0, 0) + advanceOffset) * camDist;
    float4 c4 = (float4(size.x * aspect_ratio, -size.z, 0, 0) + advanceOffset) * camDist;

    c1.xy /= _ScreenParams.xy;
    c2.xy /= _ScreenParams.xy;
    c3.xy /= _ScreenParams.xy;
    c4.xy /= _ScreenParams.xy;

    if (ProjectionFlipped())
    {
        c1.y = -c1.y;
        c2.y = -c2.y;
        c3.y = -c3.y;
        c4.y = -c4.y;
    }

    float4 p1 = centerClip + c4;
    float4 p2 = centerClip + c1;
    float4 p3 = centerClip + c2;
    float4 p4 = centerClip + c3;

    font_g2f vd1;
    vd1.pos = p1;
    vd1.uv = ci.BottomLeft;
    vd1.color = td.Color;
    vd1.scale = float2(td.Scale * aspect_ratio, td.Scale);

    font_g2f vd2;
    vd2.pos = p2;
    vd2.uv = ci.TopLeft;
    vd2.color = td.Color;
    vd2.scale = float2(td.Scale * aspect_ratio, td.Scale);

    font_g2f vd3;
    vd3.pos = p3;
    vd3.uv = ci.TopRight;
    vd3.color = td.Color;
    vd3.scale = float2(td.Scale * aspect_ratio, td.Scale);

    font_g2f vd4;
    vd4.pos = p4;
    vd4.uv = ci.BottomRight;
    vd4.color = td.Color;
    vd4.scale = float2(td.Scale * aspect_ratio, td.Scale);

    triangleStream.Append(vd1);
    triangleStream.Append(vd2);
    triangleStream.Append(vd4);
    triangleStream.Append(vd3);

    triangleStream.RestartStrip();
}

// METHODS
float aastep(float4 pos, float value, float step)
{
    float distanceToCamera = 1.0 / pos.w;
    float afwidth = step * distanceToCamera;
    return smoothstep(0.5 - afwidth, 0.5 + afwidth, value);
}

// MSDF SAMPLE METHODS
float median(float r, float g, float b)
{
    return max(min(r, g), min(max(r, g), b));
}

float screenPxRange(float scale)
{
    return (scale / _AtlasSize) * _DistanceRange;
}

float calculate_msdf(float3 msd, float scale)
{
    float sd = median(msd.r, msd.g, msd.b);
    float spx = screenPxRange(scale) * (sd - 0.5);
    float opacity = saturate(spx + 0.5);

    return smoothstep(0, 1, opacity);
}

float sample_msdf(float4 pos, float2 uv, float2 scale)
{
    float4 msd = tex2D(_MainTex, uv);
    return calculate_msdf(msd, scale);    
}

float calculate_msdf_ss(float2 uv)
{
    float3 sample = tex2D(_MainTex, uv).rgb;
    float d = 2.0 * median(sample.r, sample.g, sample.b) - 1.0;

    return step(-d, 0.0);
}

float sample_msdf_ss(float4 pos, float2 uv, float2 scale)
{
    float2 fw = fwidth(uv);
    
    float msd = calculate_msdf_ss(uv);
    msd += calculate_msdf_ss(uv - float2(0.125 * fw.x, 0.0));
    msd += calculate_msdf_ss(uv + float2(0.125 * fw.x, 0.0));
    msd += calculate_msdf_ss(uv - float2(0.0, 0.375 * fw.y));
    msd += calculate_msdf_ss(uv + float2(0.0, 0.375 * fw.y));

    return msd * rcp(3.0);
}


// SDF SAMPLE METHODS
float sample_sdf(float4 pos, float2 uv)
{
    float sd = tex2D(_MainTex, uv).a;
    float smoothing = aastep(pos, sd, 0.1);
    float opacity = smoothstep(0.5 - smoothing, 0.5 + smoothing, sd);

    return saturate(opacity * (sd > 0.5));
}