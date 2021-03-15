Shader "ReGizmo/SDFShader"
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
        #pragma target 4.6

        static const float2 pixelSize = 1.0 / _ProjectionParams.xy;
        
        float4 frag(font_g2f i) : SV_Target
        {
            float opacity = sampleMSDF(i.pos, i.uv, i.scale);
            opacity += (1 - exp2(-0.2 * opacity));
            return float4(i.color, saturate(opacity));
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