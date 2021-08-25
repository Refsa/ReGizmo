Shader "Hidden/ReGizmo/BlitDepth_URP" {
    Properties { }
    SubShader {
        ZTest Always Cull Off ZWrite Off
        
        Pass {
            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            #pragma vertex vert
            #pragma fragment frag

            TEXTURE2D(_CameraDepthAttachment);
            SAMPLER(sampler_CameraDepthAttachment_linear_clamp);
            
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
                o.pos = TransformObjectToHClip(v.vertex);
                o.uv = v.texcoord;
                
                return o;
            }
            
            float frag(v2f i) : SV_DEPTH {
                float depth = SAMPLE_TEXTURE2D(_CameraDepthAttachment, sampler_CameraDepthAttachment_linear_clamp, i.uv);
                return depth;
            }
            ENDHLSL
        }
    } 
}