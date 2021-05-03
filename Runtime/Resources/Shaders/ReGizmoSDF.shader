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
        #include "Utils/ReGizmoShaderUtils.cginc"
        #include "Utils/ReGizmoFontUtils.cginc"
        #pragma target 4.6

        static const float2 pixelSize = 1.0 / _ProjectionParams.xy;
        
        float4 frag(font_g2f i, inout uint mask : SV_COVERAGE) : SV_Target
        {
            float opacity = sampleMSDF(i.pos, i.uv, i.scale);

            mask = opacity == 0 ? 0 : 1;

            return float4(i.color, opacity);
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest On
            ZWrite On
            Cull Front
            AlphaToMask On

            CGPROGRAM
            #pragma vertex font_vert
            #pragma geometry font_geom
            #pragma fragment frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }
    }
}