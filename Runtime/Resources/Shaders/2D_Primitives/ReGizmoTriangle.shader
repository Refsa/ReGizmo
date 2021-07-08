Shader "Hidden/ReGizmo/TriangleShader"
{
    Properties { }
    SubShader
    {
        Tags { "RenderType"="Overlay" "Queue"="Overlay" }

        CGINCLUDE
        #include "../Utils/ReGizmo2DUtils.cginc"

        float4 _frag(g2f_2d i)
        {
            float sdf = sdTriangle(i.uv - float2(0.05, 0.05), float2(0.5, 1), float2(0, 0), float2(1, 0));
            sdf = sample_sdf(sdf);
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
            #pragma fragment frag_depth
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float frag_depth(g2f_2d i, out float depth : SV_DEPTH) : SV_TARGET
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
            Blend Zero OneMinusSrcAlpha
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert_2d
            #pragma geometry geom_2d
            #pragma fragment revealage_frag
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float revealage_frag(g2f_2d i) : SV_TARGET
            {
                float4 col = _frag(i);
                return col.aaaa;
            }
            ENDCG
        }
    }
}
