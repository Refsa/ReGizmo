Shader "ReGizmo/SSLine" {
	Properties {
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 2
	}
	SubShader {
		Tags {
            "RenderType" = "Transparent"
            "IgnoreProjector" = "True"
            "Queue" = "Transparent"
        }

        Cull Off
        Lighting Off
        ZWrite On
        ZTest Less
        Blend SrcAlpha OneMinusSrcAlpha 

		Pass {
			CGPROGRAM
			#pragma vertex vert
            #pragma geometry geom
			#pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
			#include "UnityCG.cginc"
			#include "../Utils/ReGizmoShaderUtils.cginc"

            struct v2g {
                float4 pos: SV_POSITION;
                uint vertexID: TEXCOORD0;
            };

            struct g2f {
				float4 pos: SV_POSITION;
                float4 color: TEXCOORD1;
                float2 uv: TEXCOORD2;
			};

            struct Properties {
                float3 Position1;
                float3 Position2;
                float4 Color;
                float Width;
            };

			StructuredBuffer<Properties> _Properties;

            void setup()
            {
                
            }

			v2g vert(appdata_full v, uint vertexID: SV_VertexID) 
            {
				v2g f;
                
                f.vertexID = vertexID;
                f.pos = UnityObjectToClipPos(v.vertex);

				return f; 
			}

            [maxvertexcount(6)]
            void geom(point v2g i[1], inout TriangleStream<g2f> triangleStream)
            {
                Properties prop = _Properties[i[0].vertexID];

                float4 clip1 = UnityObjectToClipPos(float4(prop.Position1, 1.0));
                float4 clip2 = UnityObjectToClipPos(float4(prop.Position2, 1.0));

                float2 screen1 = _ScreenParams.xy * (0.5 * clip1.xy / clip1.w + 0.5);
                float2 screen2 = _ScreenParams.xy * (0.5 * clip2.xy / clip2.w + 0.5);

                float2 xBasis = normalize(screen2 - screen1);
                float2 yBasis = float2(-xBasis.y, xBasis.x);

                float2 pt1 = screen1 + prop.Width * (i[0].pos.x * xBasis + i[0].pos.y * yBasis);
                float2 pt2 = screen2 + prop.Width * (i[0].pos.x * xBasis + i[0].pos.y * yBasis);

                float2 pt = lerp(pt1, pt2, i[0].pos.z);
                float4 clip = lerp(clip1, clip2, i[0].pos.z);

                float4 pos = float4(clip.w * (2 * pt / _ScreenParams.xy - 1.0), clip.z, clip.w);
                float4 norm = float4(normalize(cross(pos, WorldSpaceViewDir(pos))), 1.0);

                float4 c1 = UnityObjectToClipPos(pos + norm * prop.Width);
                float4 c2 = UnityObjectToClipPos(pos - norm * prop.Width);
                float4 c3 = UnityObjectToClipPos(pos - norm * prop.Width);
                float4 c4 = UnityObjectToClipPos(pos + norm * prop.Width);

                g2f g1;
                g1.pos = c1;
                g1.color = prop.Color;
                g1.uv = float2(0, 0);

                g2f g2;
                g2.pos = c2;
                g2.color = prop.Color;
                g2.uv = float2(1, 0);

                g2f g3;
                g3.pos = c3;
                g3.color = prop.Color;
                g3.uv = float2(1, 1);

                g2f g4;
                g4.pos = c4;
                g4.color = prop.Color;
                g4.uv = float2(0, 1);

                triangleStream.Append(g1);
                triangleStream.Append(g2);
                triangleStream.Append(g3);
                triangleStream.Append(g1);
                triangleStream.Append(g4);
                triangleStream.Append(g3);
                triangleStream.RestartStrip();
            }

			float4 frag(g2f g) : SV_Target
            {   
                float4 c = g.color;

				return c;
			}
			ENDCG
		}
	}
}