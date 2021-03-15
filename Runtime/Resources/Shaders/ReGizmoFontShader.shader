Shader "ReGizmo/FontShader"
{
    Properties
    {
        _MainTex ("Font Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent+1000"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
        }

        CGINCLUDE
        #include "Utils/ReGizmoShaderUtils.cginc"
        #include "Utils/ReGizmoFontUtils.cginc"

        static const float2 pixelSize = 1.0 / _ProjectionParams.xy;

        float4 frag(font_g2f i) : SV_Target
        {
            float glyph = tex2Dlod(_MainTex, float4(i.uv, 0, 0)).a;
            float4 col = float4(i.color, glyph);
            return col;
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGB
            ZTest On // Off = Overlay
            ZWrite Off
            Cull Front

            CGPROGRAM
            #pragma vertex font_vert
            #pragma geometry font_geom
            #pragma fragment frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }
    }
}