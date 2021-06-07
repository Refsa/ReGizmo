Shader "Hidden/ReGizmo/PolyLine_Screen" {
	Properties { }
	SubShader {
		Tags {
            "RenderType" = "Overlay"
            "Queue" = "Overlay"
        }
    
        CGINCLUDE
        #include "UnityCG.cginc"
        #include "Utils/ReGizmoShaderUtils.cginc"

        struct v2g {
            uint vertexID: TEXCOORD0;
            uint instanceID: TEXCOORD1;
        };
        struct g2f {
            float4 pos: SV_POSITION;
            noperspective float2 uv: TEXCOORD0;
            float4 color: TEXCOORD1;
            float width: TEXCOORD2;
        };

        StructuredBuffer<PolyLineProperties> _Properties;

        v2g vert(uint instanceID: SV_InstanceID, uint vertexID: SV_VertexID) 
        {
            v2g f;
            
            f.instanceID = instanceID;
            f.vertexID = vertexID;
            
            return f;
        }

        [maxvertexcount(4)]
        void geom(point v2g i[1], inout TriangleStream<g2f> triangleStream)
        {
            uint vidA = i[0].vertexID;
            uint vidB = vidA + 1;
            uint vidP = vidA - 1;
            uint vidN = vidA + 2;

            PolyLineProperties propA = _Properties[vidA];
            PolyLineProperties propB = _Properties[vidB];
            if (propB.ID.x != propA.ID.x) return;

            float4 p1 = UnityObjectToClipPos(float4(propA.Position, 1.0));
            float4 p2 = UnityObjectToClipPos(float4(propB.Position, 1.0));

            PolyLineProperties propP;
            PolyLineProperties propN;

            // Order points by depth
            if (p1.w > p2.w)
            {
                float4 pos_temp = p1;
                p1 = p2;
                p2 = pos_temp;

                PolyLineProperties prop_temp = propA;
                propA = propB;
                propB = prop_temp;
                
                propP = _Properties[vidN];
                propN = _Properties[vidP];
            }
            else
            {
                propP = _Properties[vidP];
                propN = _Properties[vidN];
            }

            // fix near-clip
            if (p1.w < _ProjectionParams.w)
            {
                float ratio = (_ProjectionParams.y - p1.w) / (p2.w - p1.w);
                p1 = lerp(p1, p2, ratio);
            }

            float w1 = ceil(propA.Width + PixelSize);
            float w2 = ceil(propB.Width + PixelSize);

            float2 a = p1.xy / p1.w;
            float2 b = p2.xy / p2.w;

            float2 c1 = normalize(float2(a.y - b.y, b.x - a.x)) / _ScreenParams.xy * w1;
            float2 c2 = normalize(float2(a.y - b.y, b.x - a.x)) / _ScreenParams.xy * w2;

            g2f g0 = (g2f)0;
            g2f g1 = (g2f)0;
            g2f g2 = (g2f)0;
            g2f g3 = (g2f)0;

            g0.pos = float4(p1.xy + c1 * p1.w, p1.zw);
            g1.pos = float4(p1.xy - c1 * p1.w, p1.zw);
            g2.pos = float4(p2.xy + c2 * p2.w, p2.zw);
            g3.pos = float4(p2.xy - c2 * p2.w, p2.zw);

            g0.uv = float2(0,0);
            g1.uv = float2(1,0);
            g2.uv = float2(0, 1);
            g3.uv = float2(1, 1);

            g0.color = propA.Color;
            g1.color = propA.Color;
            g2.color = propB.Color;
            g3.color = propB.Color;

            g0.width = w1;
            g1.width = w1;
            g2.width = w2;
            g3.width = w2;

            if (ProjectionFlipped())
            {
                triangleStream.Append(g1);
                triangleStream.Append(g0);
                triangleStream.Append(g3);
                triangleStream.Append(g2);
            }
            else
            {
                triangleStream.Append(g0);
                triangleStream.Append(g1);
                triangleStream.Append(g2);
                triangleStream.Append(g3);
            }
            triangleStream.RestartStrip();
        }

        float4 frag(g2f g) : SV_Target
        {   
            float4 col = g.color;

            const float2 center_uv = float2(0.5, g.uv.y);
            const float dist = distance(g.uv, center_uv) * 2;

            static const float width_factor = 0.2;
            static const float sharpness = 2.7;
            static const float smoothing = 3.5;

            float x = pow(dist, g.width * width_factor);
            col.a = exp2(-smoothing * pow(x, sharpness));

            return col;
        }
        ENDCG

		Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            ZTest LEqual

			CGPROGRAM
			#pragma vertex vert
            #pragma geometry geom
			#pragma fragment frag
            #pragma multi_compile_instancing
            ENDCG
		}
	}
}