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

        float4 _frag (g2f_2d i)
        {
            float2 fw = fwidth(i.uv);
            float2 pos = i.uv - 0.55;

            float sdf = calc_sdf(pos, 0.52, i.inner_radius);
            sdf += calc_sdf(pos - float2(0.5 * fw.x, 0.0), 0.52, i.inner_radius);
            sdf += calc_sdf(pos + float2(0.5 * fw.x, 0.0), 0.52, i.inner_radius);
            sdf += calc_sdf(pos - float2(0.0, 0.5 * fw.y), 0.52, i.inner_radius);
            sdf += calc_sdf(pos + float2(0.0, 0.5 * fw.y), 0.52, i.inner_radius);

            sdf = 1 - exp2(-3 * sdf * sdf);

            return lerp(float4(i.color.rgb, 0), i.color, sdf);
        }
        ENDCG

        Pass
        {
            Name "Render"

            Blend One One
            ZTest [_ZTest]
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert_2d
            #pragma geometry geom_2d
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float4 frag (g2f_2d i) : SV_Target
            {
                float4 col = _frag(i);

                clip(col.a == 0 ? -1 : 1);

                return col;
            }
            ENDCG
        }

        Pass
        {
            Name "Depth"

            ZTest LEqual
            ZWrite On
            Cull Off

            CGPROGRAM
            #pragma vertex vert_2d
            #pragma geometry geom_2d
            #pragma fragment depth_frag
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float depth_frag(g2f_2d i, out float depth : SV_DEPTH) : SV_TARGET
            {
                float4 col = _frag(i);
                clip(col.a == 0 ? -1 : 1);

                depth = i.pos.z;
                return depth;
            }
            ENDCG
        }

        Pass
        {
            Name "OIT_Revealage"

            ZWrite Off
            Blend Zero OneMinusSrcAlpha
            Cull Off

            CGPROGRAM
            #pragma vertex vert_2d
            #pragma geometry geom_2d
            #pragma fragment revealage_frag
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float4 revealage_frag(g2f_2d i) : SV_TARGET
            {
                float4 col = _frag(i);
                return col.aaaa;
            }
            ENDCG
        }
    }
}
