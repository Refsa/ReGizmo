#include "UnityCG.cginc"
#include "ReGizmoShaderUtils.cginc"
#include "SDF_Utils.cginc"

static const int DRAW_MODE_BILLBOARD_FREE = 1 << 0;
static const int DRAW_MODE_BILLBOARD_ALIGNED = 1 << 1;
static const int DRAW_MODE_AXIS_ALIGNED = 1 << 2;

static const int SIZE_MODE_PIXEL = 1 << 11;
static const int SIZE_MODE_PERCENT = 1 << 12;
static const int SIZE_MODE_UNIT = 1 << 13;

struct Data {
    float3 position;
    float3 normal;
    float radius;
    float thickness;
    float3 color;
    int flags;
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

bool has_flag(int mask, int flag)
{
    return (mask & flag) != 0;
}

static const float aspect_ratio = _ScreenParams.x / _ScreenParams.y;

[maxvertexcount(4)]
void geom_2d(point v2g_2d i[1], inout TriangleStream<g2f_2d> triangleStream)
{
    Data bd = _Properties[i[0].vertexID];

    float4 clip = UnityObjectToClipPos(float4(bd.position, 1.0));
    float4 cp1, cp2, cp3, cp4;
    float inner_radius = (bd.radius == bd.thickness) ? 
        -1 : 
        (bd.thickness / bd.radius) / _ScreenParams.x * clip.w * 0.5;
        
    float size = bd.radius * 1.05;

    if (has_flag(bd.flags, DRAW_MODE_AXIS_ALIGNED))
    {
        float3 normal = normalize(bd.normal);

        float3 leftright = float3(0,1,0), updown = float3(1,0,0);
        if (abs(dot(leftright, normal)) == 1.0)
        {
            leftright = float3(1,0,0);
        }
        leftright = normalize(cross(leftright, normal));
        updown = normalize(cross(normal, leftright)) * size;
        leftright *= size;

        float3 top_left = bd.position - updown - leftright;
        float3 top_right = bd.position - updown + leftright;
        float3 bottom_right = bd.position + updown + leftright;
        float3 bottom_left = bd.position + updown - leftright;

        cp1 = UnityObjectToClipPos(float4(top_left, 1.0));
        cp2 = UnityObjectToClipPos(float4(top_right, 1.0));
        cp4 = UnityObjectToClipPos(float4(bottom_left, 1.0));
        cp3 = UnityObjectToClipPos(float4(bottom_right, 1.0));
    }
    else if (has_flag(bd.flags, DRAW_MODE_BILLBOARD_ALIGNED))
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

        if (has_flag(bd.flags, SIZE_MODE_PIXEL))
        {
            size *= 0.016;
        }

        updown *= size;
        leftright *= size;

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
        float halfOffset = size;
        float2 size = float2(-halfOffset, -halfOffset);

        // Scale the size to screen coords
        if (has_flag(bd.flags, SIZE_MODE_PIXEL))
        {
            size /= _ScreenParams.xy;
            size *= clip.w;
        }

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
    
    static const float uv_width = 1.1;

    g2f_2d g1;
    g1.pos = cp1;
    g1.uv = float2(uv_width, 0);
    g1.color = bd.color;
    g1.inner_radius = inner_radius;

    g2f_2d g2;
    g2.pos = cp2;
    g2.uv = float2(uv_width, uv_width);
    g2.color = bd.color;
    g2.inner_radius = inner_radius;

    g2f_2d g3;
    g3.pos = cp3;
    g3.uv = float2(0, uv_width);
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