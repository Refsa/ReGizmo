// Source: https://github.com/Chaser324/unity-wireframe/tree/master/Assets/Wireframe/Shaders

Shader "ReGizmo/Wireframe"
{
    Properties {
        _WireThickness ("Wire Thickness", RANGE(0, 800)) = 100
		_WireSmoothness ("Wire Smoothness", RANGE(0, 20)) = 3
		_Color ("Wire Color", Color) = (0.0, 1.0, 0.0, 1.0)
		_BaseColor ("Base Color", Color) = (0.0, 0.0, 0.0, 0.0)
		_MaxTriSize ("Max Tri Size", RANGE(0, 200)) = 25
    }
    SubShader {
        Tags {
            "RenderType" = "Transparent"
            "IgnoreProjector" = "True"
            "Queue" = "Transparent+10"
        }

        Cull Off
        Lighting Off
        ZTest Always
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2g
            {
                float4 projectionSpaceVertex : SV_POSITION;
                float4 worldSpacePosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct g2f
            {
                float4 projectionSpaceVertex : SV_POSITION;
                float4 worldSpacePosition : TEXCOORD0;
                float4 dist : TEXCOORD1;
                float4 area : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            float _WireThickness;
            float _WireSmoothness;
            float4 _BaseColor;
            float _MaxTriSize;

            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
            UNITY_INSTANCING_BUFFER_END(Props)
            
            v2g vert (appdata v)
            {
                v2g o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                UNITY_TRANSFER_INSTANCE_ID(v, o);

                o.projectionSpaceVertex = UnityObjectToClipPos(v.vertex);
                o.worldSpacePosition = mul(unity_ObjectToWorld, v.vertex);

                return o;
            }

            [maxvertexcount(3)]
            void geom(triangle v2g i[3], inout TriangleStream<g2f> triangleStream)
            {
                float2 p0 = i[0].projectionSpaceVertex.xy / i[0].projectionSpaceVertex.w;
                float2 p1 = i[1].projectionSpaceVertex.xy / i[1].projectionSpaceVertex.w;
                float2 p2 = i[2].projectionSpaceVertex.xy / i[2].projectionSpaceVertex.w;

                float2 edge0 = p2 - p1;
                float2 edge1 = p2 - p0;
                float2 edge2 = p1 - p0;

                float4 worldEdge0 = i[0].worldSpacePosition - i[1].worldSpacePosition;
                float4 worldEdge1 = i[1].worldSpacePosition - i[2].worldSpacePosition;
                float4 worldEdge2 = i[0].worldSpacePosition - i[2].worldSpacePosition;

                // To find the distance to the opposite edge, we take the
                // formula for finding the area of a triangle Area = Base/2 * Height, 
                // and solve for the Height = (Area * 2)/Base.
                // We can get the area of a triangle by taking its cross product
                // divided by 2.  However we can avoid dividing our area/base by 2
                // since our cross product will already be double our area.
                float area = abs(edge1.x * edge2.y - edge1.y * edge2.x);
                float wireThickness = 800 - _WireThickness;

                g2f o;

                o.area = float4(0, 0, 0, 0);
                o.area.x = max(length(worldEdge0), max(length(worldEdge1), length(worldEdge2)));

                o.worldSpacePosition = i[0].worldSpacePosition;
                o.projectionSpaceVertex = i[0].projectionSpaceVertex;
                o.dist.xyz = float3( (area / length(edge0)), 0.0, 0.0) * o.projectionSpaceVertex.w * wireThickness;
                o.dist.w = 1.0 / o.projectionSpaceVertex.w;
                UNITY_TRANSFER_INSTANCE_ID(i[0], o);
                UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(i[0], o);
                triangleStream.Append(o);

                o.worldSpacePosition = i[1].worldSpacePosition;
                o.projectionSpaceVertex = i[1].projectionSpaceVertex;
                o.dist.xyz = float3(0.0, (area / length(edge1)), 0.0) * o.projectionSpaceVertex.w * wireThickness;
                o.dist.w = 1.0 / o.projectionSpaceVertex.w;
                UNITY_TRANSFER_INSTANCE_ID(i[1], o);
                UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(i[1], o);
                triangleStream.Append(o);

                o.worldSpacePosition = i[2].worldSpacePosition;
                o.projectionSpaceVertex = i[2].projectionSpaceVertex;
                o.dist.xyz = float3(0.0, 0.0, (area / length(edge2))) * o.projectionSpaceVertex.w * wireThickness;
                o.dist.w = 1.0 / o.projectionSpaceVertex.w;
                UNITY_TRANSFER_INSTANCE_ID(i[2], o);
                UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(i[2], o);
                triangleStream.Append(o);
            }

            fixed4 frag(g2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);

                float minDistanceToEdge = min(i.dist[0], min(i.dist[1], i.dist[2])) * i.dist[3];

                // Early out if we know we are not on a line segment.
                if(minDistanceToEdge > 0.9 || i.area.x > _MaxTriSize)
                {
                    return fixed4(_BaseColor.rgb,0);
                }

                float4 wireColor = UNITY_ACCESS_INSTANCED_PROP(Props, _Color);

                // Smooth our line out
                float t = exp2(_WireSmoothness * -1.0 * minDistanceToEdge * minDistanceToEdge);
                fixed4 finalColor = lerp(_BaseColor, wireColor, t);

                return finalColor;
            }
            ENDCG
        }
    }
}
