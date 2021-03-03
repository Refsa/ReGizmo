Shader "ReGizmo/DMIIDepthSort" {
	Properties { }
	SubShader {
		Tags {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        CGINCLUDE
            #include "UnityCG.cginc"
			#include "Utils/ReGizmoShaderUtils.cginc"

            struct vertex {
				float4 vertex	: POSITION;
				float2 uv	: TEXCOORD0;
                float3 normal : NORMAL;
			};
			struct fragment {
				float4 loc	: SV_POSITION;
				float2 uv	: TEXCOORD0;
                float4 col : TEXCOORD1;
                float dist : TEXCOORD3;
			};

        #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
			StructuredBuffer<DMIIProperties> _Properties;
        #endif
            float _Shaded;
            float _FresnelFactor;

            void setup() { }

			fragment vert(vertex v, uint instanceID: SV_InstanceID) 
            {
				fragment f; 

        #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED 
                DMIIProperties prop = _Properties[instanceID]; 
                float4 cloc = TRS(prop.Position, prop.Rotation, prop.Scale, v.vertex);
				f.loc = UnityObjectToClipPos(cloc);
                f.col = prop.Color; 

                float3 normal = normalize(UnityObjectToWorldNormal(v.normal));
                float3 viewDir = normalize(WorldSpaceViewDir(cloc));
                f.dist = 1.0 - pow((1.0 - saturate(dot(normal, viewDir))), _FresnelFactor);
                f.dist = smoothstep(0, 1, f.dist);
        #else
                float4 cloc = float4(v.vertex.xyz, 1);
                f.loc = UnityObjectToClipPos(cloc);
                f.col = float4(1, 0, 1, 1); 
                f.dist = 1;
        #endif

				return f;
			}

			float4 frag(fragment f) : SV_Target
            {
                float4 c = f.col;
                float3 shade = lerp(c.rgb, 0, _Shaded);
                c.rgb = saturate(lerp(shade, c.rgb, f.dist));

				return c * c.a;
			}
        ENDCG

        Pass {
			Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGB
            ZTest On
            ZWrite Off

            CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
			ENDCG
        }

        Pass {
			Blend SrcAlpha OneMinusSrcAlpha
            ZTest On
            ZWrite On

            CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
			ENDCG
        }
	}
}