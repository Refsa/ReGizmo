/*
MIT License

Copyright (c) 2020 Lele Feng

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

Shader "Hidden/OIT/Blend" {
    Properties {
        _MainTex ("Main Tex", 2D) = "white" {}
        _AccumTex ("Accum", 2D) = "black" {}
        _RevealageTex ("Revealage", 2D) = "white" {}
    }
    SubShader {
        ZTest Always Cull Off ZWrite Off Fog { Mode Off }
        
        Pass {
            CGPROGRAM
            #include "../Utils/ReGizmoShaderUtils.cginc"
            
            #pragma vertex vert
            #pragma fragment frag
            
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

            sampler2D _MainTex;
            sampler2D _AccumTex;
            sampler2D _RevealageTex;

            float4 frag(v2f i) : SV_Target {
                float2 uv = i.uv;

                float2 fuv = uv;
                if (ProjectionFlipped())
                {
                    fuv.y = 1 - fuv.y;
                }

                float4 background = tex2D(_MainTex, fuv);
                float4 accum = tex2D(_AccumTex, uv);
                float revealage = tex2D(_RevealageTex, uv).r;

                float4 blend = float4(accum.rgb / clamp(accum.a, 1e-4, 5e4), revealage);
                // return (1.0 - blend.a) * blend + blend.a * background;

                blend = saturate(blend);
                if (length(blend.rgb) == 0.0) return background;
                float4 col = (1.0 - blend.a) * blend + blend.a * background;
                return col;
            }
            
            ENDCG
        }
    } 
    FallBack "Transparent/VertexLit"
}