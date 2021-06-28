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

        float4 _frag(font_g2f i)
        {
            float op = tex2D(_MainTex, i.uv).a;
            float4 col = float4(i.color, smoothstep(0, 1, op));
            return col;
        }
        
        float4 frag(font_g2f i) : SV_Target
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

            CGPROGRAM
            #pragma vertex font_vert
            #pragma geometry font_geom
            #pragma fragment frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }

        Pass
        {
            ZTest [_ZTest]
            ZWrite On

            CGPROGRAM
            #pragma vertex font_vert
            #pragma geometry font_geom
            #pragma fragment frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float frag_depth(font_g2f i) : SV_TARGET1
            {
                float4 col = _frag(i);
                clip(col.a == 0 ? -1 : 1);

                return i.pos.z;
            }
            ENDCG
        }
    }
}