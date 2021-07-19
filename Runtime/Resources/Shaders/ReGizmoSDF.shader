Shader "Hidden/ReGizmo/Font_SDF"
{
    Properties
    {
        _MainTex ("Font Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "Queue"="Overlay"
            "RenderType"="Overlay"
        }

        CGINCLUDE
        #include "Utils/ReGizmoFontUtils.cginc"

        float4 _frag(font_g2f i)
        {
            #if SDF_SS
                float opacity = sample_msdf_ss(i.pos, i.uv, i.scale);
            #else
                float opacity = sample_msdf(i.pos, i.uv, i.scale);
            #endif

            return float4(i.color, saturate(opacity));
        }
        ENDCG

        Pass
        {
            Name "Render"
            Blend One One
            ZTest [_ZTest]
            ZWrite Off

            CGPROGRAM
            #pragma vertex font_vert
            #pragma geometry font_geom
            #pragma fragment frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            #pragma multi_compile _ SDF_SS

            float4 frag(font_g2f i) : SV_Target
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

            CGPROGRAM
            #pragma vertex font_vert
            #pragma geometry font_geom
            #pragma fragment depth_frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            #pragma multi_compile _ SDF_SS

            void depth_frag(font_g2f i, out float depth : SV_DEPTH)
            {
                float4 col = _frag(i);
                clip(col.a == 0 ? -1 : 1);

                depth = i.pos.z;
            }
            ENDCG
        }

        Pass
        {
            Name "OIT_Revealage"
            ZWrite Off
            Blend Zero OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex font_vert
            #pragma geometry font_geom
            #pragma fragment revealage_frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            #pragma multi_compile _ SDF_SS

            float4 revealage_frag(font_g2f i) : SV_TARGET
            {
                float4 col = _frag(i);
                clip(col.a == 0 ? -1 : 1);
                return col.aaaa;
            }
            ENDCG
        }
    }
}