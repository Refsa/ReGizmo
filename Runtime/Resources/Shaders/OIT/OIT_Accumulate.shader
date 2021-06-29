/*
MIT License

Copyright (c) 2020 Lele Feng

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

Shader "Hidden/OIT/Accumulate" {
    Properties {
        _Color ("Color Tint", Color) = (1, 1, 1, 1)
        _MainTex ("Main Tex", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
    }
    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Accumulate"}
        
        Pass {
            Tags { "LightMode"="ForwardBase" }

            ZWrite Off
            Blend One One
            
            CGPROGRAM

            #pragma shader_feature  _WEIGHTED_ON
            #pragma multi_compile _WEIGHTED0 _WEIGHTED1 _WEIGHTED2
            
            #pragma vertex vert
            #pragma fragment frag

            #include "Lighting.cginc"
            
            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _BumpMap;
            
            struct a2v {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float4 texcoord : TEXCOORD0;
            };
            
            struct v2f {
                float4 pos : SV_POSITION;
                float3 lightDir: TEXCOORD0;
                float3 viewDir : TEXCOORD1;
                float2 uv : TEXCOORD2;
                float z : TEXCOORD3;
            };

            v2f vert(a2v v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

                TANGENT_SPACE_ROTATION;
                // Transform the light direction from object space to tangent space
                o.lightDir = mul(rotation, ObjSpaceLightDir(v.vertex)).xyz;
                // Transform the view direction from object space to tangent space
                o.viewDir = mul(rotation, ObjSpaceViewDir(v.vertex)).xyz;

                // Camera-space depth
                o.z = abs(mul(UNITY_MATRIX_MV, v.vertex).z);

                return o;
            }

            float w(float z, float alpha) {
                // #ifdef _WEIGHTED0
                    return pow(z, -2.5);
                // #elif _WEIGHTED1
                //     return alpha * max(1e-2, min(3 * 1e3, 10.0/(1e-5 + pow(z/5, 2) + pow(z/200, 6))));
                // #elif _WEIGHTED2
                //     return alpha * max(1e-2, min(3 * 1e3, 0.03/(1e-5 + pow(z/200, 4))));
                // #endif
                // return 1.0;
            }
            
            float4 frag(v2f i) : SV_Target {
                fixed3 tangentLightDir = normalize(i.lightDir);
                fixed3 tangentViewDir = normalize(i.viewDir);
                fixed3 tangentNormal = UnpackNormal(tex2D(_BumpMap, i.uv));

                fixed4 albedo = tex2D(_MainTex, i.uv) * _Color;
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo.rgb;
                fixed3 diffuse = _LightColor0.rgb * albedo.rgb * max(0, dot(tangentNormal, tangentLightDir));

                float alpha = albedo.a;
                float3 C = (ambient + diffuse) * alpha;

                #ifdef _WEIGHTED_ON
                    return float4(C, alpha) * w(i.z, alpha);
                #else
                    return float4(C, alpha);
                #endif
            }
            
            ENDCG
        }
    }
}