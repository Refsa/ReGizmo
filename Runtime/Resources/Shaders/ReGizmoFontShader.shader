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
        #include "Utils/ReGizmoFontUtils.cginc"
        
        float4 frag(font_g2f i) : SV_Target
        {
            float op = tex2D(_MainTex, i.uv).a;
            float4 col = float4(i.color, smoothstep(0, 1, op));

            clip(col.a == 0 ? -1 : 1);

            return col;
        }
        ENDCG

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