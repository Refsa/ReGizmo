Shader "Hidden/ReGizmo/CircleShader"
{
    Properties { }
    SubShader
    {
        Tags { "RenderType"="Overlay" "Queue"="Overlay" }

        CGINCLUDE
        #include "../Utils/ReGizmo2DUtils.cginc"

        float calc_sdf(float2 uv, float radius, float inner_radius)
        {
            float sdf = sdCircle(uv, radius);
            if (inner_radius > 0)
            {
                sdf = (abs(sdf) - inner_radius);
            }
            sdf = sample_sdf(sdf);
            return sdf;
        }

        float4 frag (g2f_2d i) : SV_Target
        {
            float2 fw = fwidth(i.uv);
            float2 pos = i.uv - 0.55;

            float sdf = calc_sdf(pos, 0.52, i.inner_radius);
            sdf += calc_sdf(pos - float2(0.5 * fw.x, 0.0), 0.52, i.inner_radius);
            sdf += calc_sdf(pos + float2(0.5 * fw.x, 0.0), 0.52, i.inner_radius);
            sdf += calc_sdf(pos - float2(0.0, 0.5 * fw.y), 0.52, i.inner_radius);
            sdf += calc_sdf(pos + float2(0.0, 0.5 * fw.y), 0.52, i.inner_radius);

            sdf = 1 - exp2(-3 * sdf * sdf);

            //return float4(lerp(1, i.color, sdf), 1.0);
            
            clip(sdf == 0 ? -1 : 1);
            return float4(i.color, sdf);
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest LEqual
            ZWrite On

            CGPROGRAM
            #pragma vertex vert_2d
            #pragma geometry geom_2d
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }
    }
}
