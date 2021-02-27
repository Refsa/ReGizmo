Shader "ReGizmo/DMIIPolyLine" {
	Properties { }
	SubShader {
		Tags {
            "RenderType" = "Transparent"
            "IgnoreProjector" = "True"
            "Queue" = "Transparent+100"
        }
    
        Lighting Off
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha

        CGINCLUDE
        #include "UnityCG.cginc"
        #include "Utils/ReGizmoShaderUtils.cginc"

        #define WIDTH_RATIO 2.0
        #define START_FLAG 2
        #define END_FLAG 4

        struct v2g {
            float4 pos : SV_POSITION;
            uint vertexID: TEXCOORD0;
            uint instanceID: TEXCOORD1;
        };
        struct g2f {
            float4 pos: SV_POSITION;
            float2 uv: TEXCOORD0;
            float4 color: TEXCOORD1;
            float width: TEXCOORD2;
            float widthScale : TEXCOORD3;
            float camDist : TEXCOORD5;
        };

        struct Properties {
            float3 Position;
            float4 Color;
            float Width;
            float EdgeSmoothing;
            float4 ID;
        };

        StructuredBuffer<Properties> _Properties;

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
            
            return f;
        }

        g2f CreateGeomOutput(float4 clipPos, float4 color, float width, float widthScale, float2 uv, float camDist)
        {
            g2f o = (g2f)0;

            o.pos = clipPos;
            o.uv = uv;
            o.color = color;
            o.width = width;
            o.widthScale = widthScale;
            o.camDist = camDist;

            return o;
        }

        static const float2 PixelSize = 1.0 / _ScreenParams.xy;

        [maxvertexcount(12)]
        void geom(point v2g i[1], inout TriangleStream<g2f> triangleStream)
        {
            uint vidA = i[0].vertexID;
            uint vidB = vidA + 1;
            uint vidP = vidA - 1;
            uint vidN = vidA + 2;

            Properties propA = _Properties[vidA];
            Properties propB = _Properties[vidB];
            Properties propP = _Properties[vidP];
            Properties propN = _Properties[vidN];

            if (propB.ID.x != propA.ID.x) return;

            float prevSame = propA.ID.x == propP.ID.x;
            float nextSame = propB.ID.x == propN.ID.x;

            float4 pA = float4(propA.Position, 1.0);
            float4 pB = float4(propB.Position, 1.0);
            float4 pP = float4(propP.Position, 1.0);
            float4 pN = float4(propN.Position, 1.0);

            float3 dir = normalize(pB - pA);

            float cd1 = max(distance(pA, _WorldSpaceCameraPos), 1);
            float cd2 = max(distance(pB, _WorldSpaceCameraPos), 1);

            float w1 = ceil(propA.Width / 2.0) * PixelSize * 2 * cd1;
            float w2 = ceil(propB.Width / 2.0) * PixelSize * 2 * cd2;
            float w = (w1 + w2) / 2.0;

            float4 p1normal = float4( cross( WorldSpaceViewDir( pA ), dir ), 0.0 );
            float4 p2normal = float4( cross( WorldSpaceViewDir( pB ), dir ), 0.0 );

            float4 l1 = float4(normalize(p1normal.xyz), 0.0) * w;
            float4 l2 = float4(normalize(p2normal.xyz), 0.0) * w;

            float4 cp1 = UnityObjectToClipPos(pA - l1);
            float4 cp2 = UnityObjectToClipPos(pA + l1);
            float4 cp3 = UnityObjectToClipPos(pB + l2);
            float4 cp4 = UnityObjectToClipPos(pB - l2);

            g2f g1 = CreateGeomOutput(cp1, propA.Color, propA.Width, 1, float2(0, 0), 1);
            g2f g2 = CreateGeomOutput(cp2, propA.Color, propA.Width, 1, float2(1, 0), 1);
            g2f g3 = CreateGeomOutput(cp3, propB.Color, propB.Width, 1, float2(1, 1), 1);
            g2f g4 = CreateGeomOutput(cp4, propB.Color, propB.Width, 1, float2(0, 1), 1);

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
            float4 col = g.color;
            col.a = 0;

            float pixelWidth = length(fwidth(g.uv)) * 0.5;

            float2 centerUV = float2(0.5, g.uv.y);
            float dist = distance(centerUV, g.uv);
            float width = pixelWidth * g.width;

            // if (dist > width) return float4(1,0,1,1);

            if (dist <= width) col.a = 1;
            else if (dist < width + pixelWidth) col.a = 0.15;

            return col; 
        }
        ENDCG

		Pass {
            ZWrite Off
            ZTest Always
			CGPROGRAM
			#pragma vertex vert
            #pragma geometry geom
			#pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            ENDCG
		}

        Pass {
            ZWrite Off
            ZTest Less
			CGPROGRAM
			#pragma vertex vert
            #pragma geometry geom
			#pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            ENDCG
		}
	}
}