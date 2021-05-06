Shader "Unlit/ReGizmoCircleShader"
{
    Properties { }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Cull Off Lighting Off Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        CGINCLUDE
        #include "UnityCG.cginc"
        #include "Utils/ReGizmoShaderUtils.cginc"
        #pragma multi_compile_instancing

        struct appdata
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f
        {
            float4 vertex : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        struct properties {
            float position;
            float radius;
            float thickness;
            float billboard;
            float3 color;
        };

        v2f vert (appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = v.uv;
            return o;
        }

        float4 frag (v2f i) : SV_Target
        {
            float centerDist = distance(i.uv, float2(0.5, 0.5));
            float4 color = float4(1,0,0,0);
            float pixelWidth = length(fwidth(i.uv)) * 0.5;
            
            if (centerDist > (0.5 - pixelWidth) && centerDist < 0.5) color.a = 1;

            return color;
        }
        ENDCG

        Pass
        {
            ZTest Always
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }

        Pass
        {
            ZTest Less
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
}
