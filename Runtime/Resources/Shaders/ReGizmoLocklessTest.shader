Shader "ReGizmo/LocklessTest" {
	Properties {
        // _Color("Color", Color) = (1, 0, 1, 1)
        // _Position("Position", Vector) = (0, 0, 0, 0)
        // _Rotation("Rotation", Vector) = (0, 0, 0, 0)
        // _Size("Size", Vector) = (1, 1, 1, 0)
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

			struct v2g {
                float4 pos : SV_POSITION;
                uint vertexID: TEXCOORD0;
                uint instanceID: TEXCOORD1;
                float4x4 scaleMat: TEXCOORD2;
            };
			struct g2f {
				float4 pos: SV_POSITION;
                uint vertexID: TEXCOORD3;
			};

            struct Properties {
                float3 Position;
                float3 Rotation;
                float3 Scale;
                float4 Color;
            };

            struct Vertex
            {
                float3 Position;
            };

            struct Triangle
            {
                Vertex vertices[3];
            };

			StructuredBuffer<Properties> _Properties;
            StructuredBuffer<Triangle> _Triangles;

            void CreateTriangleStreamData(in v2g i, in Properties prop, in Vertex vert, out g2f vertexData)
            {
                vertexData = (g2f)0;

                vertexData.vertexID = i.vertexID;

                float3 pos = mul(i.scaleMat, vert.Position);
                vertexData.pos = UnityObjectToClipPos(float4(pos + prop.Position, 1.0));
            }

            void setup()
            {

            }

			v2g vert(appdata_full v, uint instanceID: SV_InstanceID, uint vertexID: SV_VertexID) 
            {
				v2g f;
                
                Properties prop = _Properties[vertexID];

                f.instanceID = instanceID;
                f.vertexID = vertexID;
                f.pos = float4(prop.Position, 1.0);
                f.scaleMat = 
                    float4x4(
                        prop.Scale.x, 0, 0, 0,
                        0, prop.Scale.y, 0, 0,
                        0, 0, prop.Scale.z, 0,
                        0, 0, 0, 0
                    );

				return f;
			}

            [maxvertexcount(36)]
            void geom(point v2g input[1], inout TriangleStream<g2f> triangleStream)
            {
                Properties prop = _Properties[input[0].vertexID];

                g2f vertexData = (g2f)0;

                for (int i = 0; i < 12; i++) 
                {
                    Triangle tri = _Triangles[i];

                    // tri.vertices[0].Position += prop.Position;
                    // tri.vertices[1].Position += prop.Position;
                    // tri.vertices[2].Position += prop.Position;

                    CreateTriangleStreamData(input[0], prop, tri.vertices[0], vertexData);
                    triangleStream.Append(vertexData);

                    CreateTriangleStreamData(input[0], prop, tri.vertices[1], vertexData);
                    triangleStream.Append(vertexData);

                    CreateTriangleStreamData(input[0], prop, tri.vertices[2], vertexData);
                    triangleStream.Append(vertexData);
                }
            }

			float4 frag(g2f f) : SV_Target{
                Properties prop = _Properties[f.vertexID];

                float4 c = prop.Color;

				return c;
			}
			ENDCG
		}
	}
}