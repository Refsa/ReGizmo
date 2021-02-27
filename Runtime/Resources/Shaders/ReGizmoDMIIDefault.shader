Shader "ReGizmo/DMIIDefault" {
	Properties { 
        [HideInInspector] _ZWrite("__zw", Float) = 0.0
        [HideInInspector] _ZTest("__zt", Float) = 0.0
    }
	SubShader {
		Tags {
            "RenderType" = "Opaque"
            "IgnoreProjector" = "True"
            "Queue" = "Transparent"
        }
        Lighting Off
        Blend SrcAlpha OneMinusSrcAlpha

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

            void setup()
            {
        #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
                DMIIProperties prop = _Properties[unity_InstanceID];
        #endif
            }

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
                f.dist = distance(prop.Position, _WorldSpaceCameraPos);
                f.dist = 1.0 - pow((1.0 - saturate(dot(normal, viewDir))), _FresnelFactor) * (1 - clamp(f.dist / 100, 0, 1));
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

                float fresnel = smoothstep(0, 1, f.dist);
                float3 shade = lerp(c.rgb, 0, _Shaded);

                c.rgb = saturate(lerp(shade, c.rgb, fresnel));

				return c * c.a;
			}
        ENDCG

        Pass {
            ZTest[_ZTest] ZWrite[_ZWrite]

            CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
			ENDCG
        }
	}
}