
inline float4 oit_blend(float4 background, float4 accum, float revealage) 
{
    float4 blend = float4(accum.rgb / clamp(accum.a, 1e-4, 5e4), revealage);
    // return (1.0 - blend.a) * blend + blend.a * background;

    blend = saturate(blend);
    if (length(blend.rgb) == 0.0) return background;
    float4 col = (1.0 - blend.a) * blend + blend.a * background;
    return col;
}