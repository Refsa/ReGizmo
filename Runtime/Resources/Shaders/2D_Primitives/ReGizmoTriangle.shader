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
            float sdf = sdEquilateralTriangle(i.uv - float2(0.55, 0.29), 0.56);
            sdf = sample_sdf(sdf);
            return lerp(float4(i.color.rgb, 0), i.color, sdf);
        }

        float4 frag (g2f_2d i) : SV_Target
        {
            float4 col = _frag(i);
            clip(col.a == 0 ? -1 : 1);
            return col;
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest [_ZTest]
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert_2d
            #pragma geometry geom_2d
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }

        Pass
        {
            ZTest LEqual
            ZWrite On
            Cull Off

            CGPROGRAM
            #pragma vertex vert_2d
            #pragma geometry geom_2d
            #pragma fragment frag_depth
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float frag_depth(g2f_2d i) : SV_TARGET1
            {
                float4 col = _frag(i);
                clip(col.a == 0 ? -1 : 1);
                return i.pos.z;
            }
            ENDCG
        }
    }
}
