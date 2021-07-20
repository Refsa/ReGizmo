Shader "Hidden/ReGizmo/BlitDepth" {
    Properties {
        _MainTex ("Main Tex", 2D) = "white" {}
    }
    SubShader {
        ZTest Always Cull Off ZWrite Off
        
        Pass {
            CGPROGRAM
            #include "Utils/ReGizmoShaderUtils.cginc"
            
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _CameraDepthAttachment;
            
            struct a2v {
                float4 vertex : POSITION;
                float4 texcoord : TEXCOORD0;
            };
            
            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            v2f vert(a2v v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                
                return o;
            }
            
            float frag(v2f i) : SV_DEPTH {
                return tex2D(_CameraDepthAttachment, i.uv);
            }
            
            ENDCG
        }
    } 
}