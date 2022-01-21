Shader "Hidden/ReGizmo/BlitDepth_URP" {
    Properties { }
    SubShader {
        ZTest Always ZWrite On Cull Back
        
        Pass {
            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
            
            #pragma vertex vert
            #pragma fragment frag

            struct appdata {
                float4 vertex : POSITION;
            };
            
            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            v2f vert(appdata v) {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex);
                float4 suv = ComputeScreenPos(o.pos);
                o.uv = suv.xy / suv.w;
                
                return o;
            }
            
            float4 frag(v2f i, out float depth : SV_DEPTH) : SV_TARGET0 {
                depth = SampleSceneDepth(i.uv);
                return depth;
            }
            ENDHLSL
        }
    } 
}