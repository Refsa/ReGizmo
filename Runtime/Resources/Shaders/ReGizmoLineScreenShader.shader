// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/ReGizmo/Line_Screen"
{
    Properties
    {
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        CGINCLUDE
        #include "UnityCG.cginc"
        #include "Utils/ReGizmoShaderUtils.cginc"

        struct v2g
        {
            uint vertexID: TEXCOORD0;
        };

        struct g2f
        {
            float4 pos: SV_POSITION;
            float2 uv : TEXCOORD0;
            float3 widths : TEXCOORD1;
            float4 color: TEXCOORD2;
        };

        struct FragOut
        {
            float4 color : SV_TARGET;
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

        struct Properties
        {
            float3 Position;
            float4 Color;
            float Width;
        };

        StructuredBuffer<Properties> _Properties;

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

        [maxvertexcount(6)]
        void geom(line v2g i[2], inout TriangleStream<g2f> triangleStream)
        {
            Properties prop1 = _Properties[i[0].vertexID];
            Properties prop2 = _Properties[i[1].vertexID];

            float4 p1 = float4(prop1.Position, 1.0);
            float4 p2 = float4(prop2.Position, 1.0);
            float4 dir = normalize(p2 - p1);

            float cd1 = distance(p1, _WorldSpaceCameraPos);
            float cd2 = distance(p2, _WorldSpaceCameraPos);

            float w1 = prop1.Width * PixelSize * cd1;
            float w2 = prop2.Width * PixelSize * cd2;

            float w = (w1 + w2) * (unity_OrthoParams.w == 1.0 ? 1.0 : 0.5);

            float4 p1normal = float4(cross(WorldSpaceViewDir(p1), dir), 0.0);
            float4 p2normal = float4(cross(WorldSpaceViewDir(p2), dir), 0.0);

            float4 l1 = float4(normalize(p1normal.xyz), 0.0) * w;
            float4 l2 = float4(normalize(p2normal.xyz), 0.0) * w;

            float w1s = prop1.Width / w;
            float w2s = prop2.Width / w;

            float4 cp1 = UnityObjectToClipPos(p1 - l1);
            float4 cp2 = UnityObjectToClipPos(p1 + l1);
            float4 cp3 = UnityObjectToClipPos(p2 + l2);
            float4 cp4 = UnityObjectToClipPos(p2 - l2);

            g2f g1 = CreatePixelWidthOutput(cp1, float2(0, 0), prop1.Color, prop1.Width, w1s);
            g1.widths.z = cd1;
            g2f g2 = CreatePixelWidthOutput(cp2, float2(1, 0), prop1.Color, prop1.Width, w1s);
            g2.widths.z = cd1;
            g2f g3 = CreatePixelWidthOutput(cp3, float2(1, 1), prop2.Color, prop2.Width, w2s);
            g3.widths.z = cd2;
            g2f g4 = CreatePixelWidthOutput(cp4, float2(0, 1), prop2.Color, prop2.Width, w2s);
            g4.widths.z = cd2;

            triangleStream.Append(g1);
            triangleStream.Append(g2);
            triangleStream.Append(g3);
            triangleStream.Append(g1);
            triangleStream.Append(g4);
            triangleStream.Append(g3);
            triangleStream.RestartStrip();
        }

        float sdSegment(in float2 p, in float2 a, in float2 b)
        {
            float2 pa = p - a, ba = b - a;
            float h = clamp(dot(pa, ba) / dot(ba, ba), 0.0, 1.0);
            return length(pa - ba * h);
        }

        void Unity_Remap_float(float2 In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }

        float4 frag(g2f g) : SV_Target
        {
            float4 col = g.color;

            /*float pixelWidth = length(fwidth(g.uv)) * 0.5;
            float sd = 1 - sdSegment(g.uv, float2(0.5, 0), float2(0.5, 1));
            Unity_Remap_float(sd, float2(0, 1), float2(0.25, 0.75), sd);
            sd = smoothstep(0.5, 1, sd);

            col.a = saturate(sd);*/

            //if (col.a <= 0) return 1;

            float pixelWidth = length(fwidth(g.uv));

            float2 centerUV = float2(0.5, g.uv.y);
            float dist = distance(centerUV, g.uv);
            float width = pixelWidth * g.widths.y * 0.5;

            //if (dist > width) return float4(1,0,1,1);

            if (dist > width) col.a = 0;
            clip(col.a == 0 ? -1 : 1);

            return col;
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            ZTest On

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            ZTest On

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }
    }
}