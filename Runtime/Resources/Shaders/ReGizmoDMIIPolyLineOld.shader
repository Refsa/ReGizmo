Shader "ReGizmo/DMIIPolyLineOld" {
	Properties {
        // _Color("Color", Color) = (1, 0, 1, 1)
        // _Position("Position", Vector) = (0, 0, 0, 0)
        // _Rotation("Rotation", Vector) = (0, 0, 0, 0)
        // _Size("Size", Vector) = (1, 1, 1, 0)
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 2
	}
	SubShader {
		Tags {
            "RenderType" = "Transparent"
            "IgnoreProjector" = "True"
            "Queue" = "Transparent"
            "ReGizmoTarget" = "DMII"
            "ReGizmoProperties" = "Properties"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [_ZTest]
        Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
            #pragma geometry geom
			#pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
			#include "UnityCG.cginc"
			#include "Utils/ReGizmoShaderUtils.cginc"

            #define WIDTH_RATIO 2.0

            struct v2g {
                float4 pos: SV_POSITION;
                uint instanceID: TEXCOORD0;
            };

			struct g2f {
				float4 pos: SV_POSITION;
                float width: TEXCOORD0;
                float4 color: TEXCOORD1;
                float2 uv: TEXCOORD2;
                float4 worldPos: TEXCOORD4;
			};

            struct Properties {
                float3 Position;
                float4 Color;
                float Width;
                float EdgeSmoothing;
            };

            struct DrawData {
                int Start;
                int Count;
            };

			StructuredBuffer<Properties> _Properties;
            StructuredBuffer<DrawData> _DrawData;

            void setup()
            {
                
            }

			v2g vert(appdata_full v, uint instanceID: SV_InstanceID) 
            {
				v2g f;
                
                f.instanceID = instanceID;
                f.pos = float4(0,0,0,1);
                
				return f;
			}

            [maxvertexcount(62)]
            void geom(point v2g p[1], inout TriangleStream<g2f> triangleStream)
            {
                DrawData drawData = _DrawData[p[0].instanceID];

                int total = drawData.Start + drawData.Count;

                Properties prevProp = _Properties[drawData.Start - 1];
                Properties prop = _Properties[drawData.Start];
                Properties nextProp = _Properties[drawData.Start + 2];

                float prevUv = 0.0;
                int index = 0;

                for (int i = drawData.Start; i < drawData.Start + drawData.Count; i++)
                {
                    int isEnd = 1 - ((i + 1) >= total);
                    int isStart = 1 - (i - 1) < drawData.Start;
                    int isMiddle = !isEnd && !isStart;

                    float w2 = prop.Width / 2;

                    float4 p1 = float4(prevProp.Position, 1.0);
                    float4 p2 = float4(prop.Position, 1.0);
                    float4 p3 = float4(nextProp.Position, 1.0);

                    float4 prevDir = normalize(p1 - p2) * isStart;
                    float4 nextDir = normalize(p3 - p2) * isEnd;

                    float dirChange = dot(prevDir, nextDir);

                    float4 l1 = prevDir * 0.5;
                    float4 l2 = nextDir * 0.5;

                    float4 c1 = p2 + l2 + l1 * isEnd;
                    float4 c2 = p2 - l2 - l1 * isEnd;

                    float uv = i / total;

                    g2f g1;
                    g1.pos = UnityObjectToClipPos(c1);
                    g1.color = prop.Color;
                    g1.width = 1.0;
                    g1.uv = float2(1, uv);
                    g1.worldPos = c1;

                    g2f g2;
                    g2.pos = UnityObjectToClipPos(c2);
                    g2.color = prop.Color;
                    g2.width = 1.0;
                    g2.uv = float2(0, uv);
                    g2.worldPos = c2;

                    triangleStream.Append(g1);
                    triangleStream.Append(g2);

                    prevProp = prop;
                    prop = _Properties[i + 1];
                    nextProp = _Properties[i + 2];
                    prevUv = uv;

                    index++;
                }
            }

			float4 frag(g2f g) : SV_Target
            {   
                /* float2 uv = g.uv;
                float2 centeruv = float2(0.5, uv.y);
                float centerDist = 1.0 - (distance(uv, centeruv));

                if (centerDist < 0.94) return fixed4(0, 0, 0, 0); */

				return g.color;
			}
			ENDCG
		}
	}
}