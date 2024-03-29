
// --- MESH ---
struct MeshProperties
{
    float3 Position;
    float4 Rotation;
    float3 Scale;
    float4 Color;
};

// --- TEXT ---
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

// --- 2D PRIMITIVES ---
struct Data2D {
    float3 position;
    float3 normal;
    float radius;
    float4 color;
    int flags;
};

// --- ICON ---
struct IconProperties
{
    float3 position;
    float3 color;
    float scale;
    int flags;
};

// --- SPRITE ---
struct SpriteProperties
{
    float3 position;
    float scale;
    // xMin, yMin, xMax, yMax
    float4 uvs;
};

// --- LINES ---
struct LineProperties
{
    float3 Position1;
    float3 Position2;
    float3 Color;
    float Width;
};

// --- POLY LINE ---
struct PolyLineProperties 
{
    float3 Position;
    float4 Color;
    float Width;
    int ID;
};