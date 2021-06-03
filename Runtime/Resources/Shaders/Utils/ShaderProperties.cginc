
struct MeshProperties
{
    float3 Position;
    float4 Rotation;
    float3 Scale;
    float4 Color;
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

struct CharacterInfo
{
    float2 BottomLeft;
    float2 BottomRight;
    float2 TopLeft;
    float2 TopRight;
    float4 Size;
    float Advance;
};