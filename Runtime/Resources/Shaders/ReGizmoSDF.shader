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
        #pragma target 4.6

        float4 frag(font_g2f i) : SV_Target
        {
            float opacity = sampleMSDF(i.pos, i.uv, i.scale);

            return float4(i.color, opacity);
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest LEqual
            ZWrite Off

            CGPROGRAM
            #pragma vertex font_vert
            #pragma geometry font_geom
            #pragma fragment frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest LEqual
            ZWrite On

            CGPROGRAM
            #pragma vertex font_vert
            #pragma geometry font_geom
            #pragma fragment frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }
    }
}