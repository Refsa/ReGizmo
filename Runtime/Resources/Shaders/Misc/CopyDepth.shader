// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Hidden/ReGizmo/CopyDepth" {
    Properties { }
    SubShader {
        Pass{
            ZTest Always Cull Off ZWrite On

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"

            UNITY_DECLARE_DEPTH_TEXTURE(_DepthTex);

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            bool ProjectionFlipped()
            {
                #if UNITY_UV_STARTS_AT_TOP
                    if (_ProjectionParams.x > 0)
                    {
                        return true;
                    }
                #endif

                return false;
            }

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord.xy;
                return o;
            }

            void frag(v2f i, out float oDepth : SV_Depth)
            {
                oDepth = SAMPLE_RAW_DEPTH_TEXTURE(_DepthTex, i.texcoord);
                // return float4(oDepth, 0, 0, 1);
            }
            ENDCG

        }

    }
    Fallback Off
}
