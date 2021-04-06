Shader "Hidden/ReGizmo/Font"
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

        float4 frag(font_g2f i) : SV_Target
        {
            float op = tex2D(_MainTex, i.uv).a;
            float4 col = float4(i.color, saturate(op));
            return col;
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest On
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