// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ReGizmo/DMIILine" {
	Properties {
        // [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 2
	}
	SubShader {
		Tags {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        CGINCLUDE
        #include "UnityCG.cginc"
        #include "Utils/ReGizmoShaderUtils.cginc"

        struct v2g {
            uint vertexID: TEXCOORD0;
        };
        struct g2f {
            float4 pos: SV_POSITION;
            float2 uv : TEXCOORD0;
            float3 widths : TEXCOORD1;
            float4 color: TEXCOORD2;
            uint mode: TEXCOORD3;
        };

        g2f CreatePixelWidthOutput(float4 clipPos, float2 uv, float4 color, float width, float widthScale)
        {
            g2f g = (g2f)0;
            g.pos = clipPos;
            g.color = color;
            g.uv = uv;
            g.widths = float3(widthScale, width, 0);
            return g;
        }

        g2f CreateWorldWidthOutput(float4 clipPos, float2 uv, float4 color, float3 widths)
        {
            g2f g = (g2f)0;
            g.pos = clipPos;
            g.color = color;
            g.uv = uv;
            g.widths = widths;
            return g;
        }

        #define PixelWidthMode 1 << 0
        #define WorldWidthMode 1 << 1

        struct Properties {
            float3 Position;
            float4 Color;
            float Width;
            uint Mode;
        };

        StructuredBuffer<Properties> _Properties;

        void setup() { }

        v2g vert(appdata_full v, uint vertexID: SV_VertexID) 
        {
            v2g f = (v2g)0;
            f.vertexID = vertexID;
            return f;
        }

        static const float E = 2.71828;
        static const float HalfE = 2.71828 * 0.5;
        static const float2 PixelSize = 1.0 / _ScreenParams.xy;

        float GetDepth(float4 clipPos)
        {
            float4 screenPos = ComputeScreenPos(clipPos);
            return UNITY_Z_0_FAR_FROM_CLIPSPACE(screenPos.z);
        }

        void geomWorld(line v2g i[2], inout TriangleStream<g2f> triangleStream)
        {
            Properties prop1 = _Properties[i[0].vertexID];
            Properties prop2 = _Properties[i[1].vertexID];

            float widthAvg = (prop1.Width + prop2.Width) * 0.25;

            float4 p1 = float4(prop1.Position, 1.0);
            float4 p2 = float4(prop2.Position, 1.0);
            float4 dir = normalize(p2 - p1);

            float cd1 = distance(p1, _WorldSpaceCameraPos);
            float cd2 = distance(p2, _WorldSpaceCameraPos);
            float cdAvg = (cd1 + cd2) / 2.0;

            // float w = widthAvg * cdAvg * _ProjectionParams.w;
            float w = widthAvg * log(cdAvg * log(rcp(widthAvg)));
            w = max(w, widthAvg);

            float4 p1normal = float4( cross( WorldSpaceViewDir( p1 ), dir ), 0.0 );
            float4 p2normal = float4( cross( WorldSpaceViewDir( p2 ), dir ), 0.0 );

            float4 l1 = float4(normalize(p1normal.xyz), 0.0) * w;
            float4 l2 = float4(normalize(p2normal.xyz), 0.0) * w;

            float4 cp1 = UnityObjectToClipPos(p1 - l1);
            float4 cp2 = UnityObjectToClipPos(p1 + l1);
            float4 cp3 = UnityObjectToClipPos(p2 + l2);
            float4 cp4 = UnityObjectToClipPos(p2 - l2);

            float3 widths = float3(w, cdAvg, widthAvg / w);

            g2f g1 = CreateWorldWidthOutput(cp1, float2(0,0), prop1.Color, widths);
            g1.mode = WorldWidthMode;
            g2f g2 = CreateWorldWidthOutput(cp2, float2(1,0), prop1.Color, widths);
            g2.mode = WorldWidthMode;
            g2f g3 = CreateWorldWidthOutput(cp3, float2(1,1), prop2.Color, widths);
            g3.mode = WorldWidthMode;
            g2f g4 = CreateWorldWidthOutput(cp4, float2(0,1), prop2.Color, widths);
            g4.mode = WorldWidthMode;

            triangleStream.Append(g1);
            triangleStream.Append(g2);
            triangleStream.Append(g3);
            triangleStream.Append(g1);
            triangleStream.Append(g4);
            triangleStream.Append(g3);
            triangleStream.RestartStrip();
        }

        void geomConst(line v2g i[2], inout TriangleStream<g2f> triangleStream)
        {
            Properties prop1 = _Properties[i[0].vertexID];
            Properties prop2 = _Properties[i[1].vertexID];

            float4 p1 = float4(prop1.Position, 1.0);
            float4 p2 = float4(prop2.Position, 1.0);
            float4 dir = normalize(p2 - p1);

            float cd1 = max(distance(p1, _WorldSpaceCameraPos), 1); 
            float cd2 = max(distance(p2, _WorldSpaceCameraPos), 1);

            float w1 = ceil(prop1.Width / 2.0) * PixelSize * 2 * cd1;
            float w2 = ceil(prop2.Width / 2.0) * PixelSize * 2 * cd2;
            float w = (w1 + w2) * 0.5;

            float4 p1normal = float4( cross( WorldSpaceViewDir( p1 ), dir ), 0.0 );
            float4 p2normal = float4( cross( WorldSpaceViewDir( p2 ), dir ), 0.0 );

            float4 l1 = float4(normalize(p1normal.xyz), 0.0) * w;
            float4 l2 = float4(normalize(p2normal.xyz), 0.0) * w;

            float w1s = w1 / prop1.Width;
            float w2s = w2 / prop2.Width;

            float4 cp1 = UnityObjectToClipPos(p1 - l1);
            float4 cp2 = UnityObjectToClipPos(p1 + l1);
            float4 cp3 = UnityObjectToClipPos(p2 + l2);
            float4 cp4 = UnityObjectToClipPos(p2 - l2);

            g2f g1 = CreatePixelWidthOutput(cp1, float2(0,0), prop1.Color, ceil(prop1.Width), w1s);
            g1.mode = PixelWidthMode;
            g1.widths.z = cd1;
            g2f g2 = CreatePixelWidthOutput(cp2, float2(1,0), prop1.Color, ceil(prop1.Width), w1s);
            g2.mode = PixelWidthMode;
            g2.widths.z = cd1;
            g2f g3 = CreatePixelWidthOutput(cp3, float2(1,1), prop2.Color, ceil(prop2.Width), w2s);
            g3.mode = PixelWidthMode;
            g3.widths.z = cd2;
            g2f g4 = CreatePixelWidthOutput(cp4, float2(0,1), prop2.Color, ceil(prop2.Width), w2s);
            g4.mode = PixelWidthMode;
            g4.widths.z = cd2;

            triangleStream.Append(g1);
            triangleStream.Append(g2);
            triangleStream.Append(g3);
            triangleStream.Append(g1);
            triangleStream.Append(g4);
            triangleStream.Append(g3);
            triangleStream.RestartStrip();
        }

        [maxvertexcount(6)]
        void geom(line v2g i[2], inout TriangleStream<g2f> triangleStream)
        {
            uint mode = _Properties[i[0].vertexID].Mode;
            if (mode == PixelWidthMode)
            {
                geomConst(i, triangleStream);
            }
            else if (mode == WorldWidthMode)
            {
                geomWorld(i, triangleStream);
            }
        }
 
        float4 fragWorld(g2f g) : SV_Target
        {
            float4 col = g.color;

            float pixelWidth = length(fwidth(g.uv)) * 0.5;
            float variance = AreaPixelVariance(g.uv);
            float pw = variance / _ScreenParams.zw;

            float2 centerUV = float2(0.5, g.uv.y);
            float dist = distance(centerUV, g.uv);

            col.a = 1 - dist * rcp(g.widths.z * 0.5);
            col.a = smoothstep(0, g.widths.z * rcp(g.widths.x * 4), col.a);
            col.a = saturate(col.a + variance) * (variance < _ScreenParams.zw);

            float camFade = saturate((g.widths.y * (1 - g.widths.x)) / 120);
            col.a = lerp(col.a, 0, camFade);

            return col;
        }

        float4 fragConst(g2f g) : SV_Target
        {   
            float4 col = g.color;
            col.a = 0;

            float pixelWidth = length(fwidth(g.uv)) * 0.5;

            float2 centerUV = float2(0.5, g.uv.y);
            float dist = distance(centerUV, g.uv);
            float width = pixelWidth * g.widths.y;

            if (dist > width) return float4(1,0,1,1);

            if (dist <= width) col.a = 1;
            // else {
            //     dist = clamp(dist, dist, dist + pixelWidth);
            //     float smoothFactor = exp2(-20 * dist * dist);
            //     col.a = smoothFactor;
            // }

            return col; 
        }

        float4 frag(g2f g) : SV_Target
        {   
            if (g.mode == PixelWidthMode)
            {
                return fragConst(g);
            }
            else if (g.mode == WorldWidthMode)
            {
                return fragWorld(g);
            }

            return float4(1,0,1,1);
        }
        ENDCG


		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGB
            ZWrite Off
            ZTest On

			CGPROGRAM
			#pragma vertex vert
            #pragma geometry geom
			#pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            ENDCG
		}

        Pass {
			Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            ZTest On

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