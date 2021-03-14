Shader "ReGizmo/SDFShader"
{
    Properties
    {
        _MainTex ("Font Texture", 2D) = "white" {}
    }

    SubShader
    {

        Tags
        {
            "Queue"="Transparent+1000"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
        }

        CGINCLUDE
        #include "UnityCG.cginc"

        struct appdata_t
        {
            float4 vertex : POSITION;
            float4 color : COLOR;
        };

        struct v2g
        {
            float4 vertex : SV_POSITION;
            uint vertexID : TEXCOORD0;
        };

        struct g2f
        {
            float4 pos: SV_POSITION;
            float2 uv: TEXCOORD0;
            float3 color: TEXCOORD1;
            float2 texSize : TEXCOORD2;
            uint vertexID: TEXCOORD4;
        };

        struct CharacterInfo
        {
            float2 BottomLeft;
            float2 BottomRight;
            float2 TopLeft;
            float2 TopRight;
            float4 Size;
            float Advance;
        };

        struct CharData
        {
            uint TextID;
            uint CharIndex;
            float Advance;
        };

        struct TextData
        {
            float3 Color;
            float Scale;
            float3 Position;
            float CenterOffset;
        };

        StructuredBuffer<CharData> _CharData;
        StructuredBuffer<CharacterInfo> _CharacterInfos;
        StructuredBuffer<TextData> _TextData;

        float _DistanceRange;
        float2 _AltasSize;

        sampler2D _MainTex;
        float4 _MainTex_ST;
        float4 _MainTex_TexelSize;

        v2g vert(appdata_t v, uint vid : SV_VertexID)
        {
            CharData cd = _CharData[vid];
            TextData td = _TextData[cd.TextID];

            v2g o;
            o.vertex = float4(td.Position, 1.0);
            o.vertexID = vid;

            return o;
        }

        [maxvertexcount(6)]
        void geom(point v2g i[1], inout TriangleStream<g2f> triangleStream)
        {
            uint vid = i[0].vertexID;

            CharData cd = _CharData[vid];
            TextData td = _TextData[cd.TextID];
            CharacterInfo ci = _CharacterInfos[cd.CharIndex];

            float4 advanceOffset = float4(cd.Advance - td.CenterOffset, 0, 0, 0);

            float4 centerClip = UnityObjectToClipPos(i[0].vertex);

            float4 c1 = float4(ci.Size.x, -ci.Size.w, 0, 0) * td.Scale + advanceOffset;
            float4 c2 = float4(ci.Size.y, -ci.Size.w, 0, 0) * td.Scale + advanceOffset;
            float4 c3 = float4(ci.Size.y, ci.Size.z, 0, 0) * td.Scale + advanceOffset;
            float4 c4 = float4(ci.Size.x, ci.Size.z, 0, 0) * td.Scale + advanceOffset;

            float4 p1 = centerClip + c4;
            float4 p2 = centerClip + c1;
            float4 p3 = centerClip + c2;
            float4 p4 = centerClip + c3;

            float2 texSize = float2(ci.Size.y - ci.Size.x, ci.Size.w - ci.Size.z) / _MainTex_TexelSize.xy;

            g2f vd1;
            vd1.pos = (p1);
            vd1.uv = ci.BottomLeft;
            vd1.vertexID = i[0].vertexID;
            vd1.color = td.Color;
            vd1.texSize = texSize;

            g2f vd2;
            vd2.pos = (p2);
            vd2.uv = ci.TopLeft;
            vd2.vertexID = i[0].vertexID;
            vd2.color = td.Color;
            vd2.texSize = texSize;

            g2f vd3;
            vd3.pos = (p3);
            vd3.uv = ci.TopRight;
            vd3.vertexID = i[0].vertexID;
            vd3.color = td.Color;
            vd3.texSize = texSize;

            g2f vd4;
            vd4.pos = (p4);
            vd4.uv = ci.BottomRight;
            vd4.vertexID = i[0].vertexID;
            vd4.color = td.Color;
            vd4.texSize = texSize;

            triangleStream.Append(vd1);
            triangleStream.Append(vd2);
            triangleStream.Append(vd3);

            triangleStream.Append(vd1);
            triangleStream.Append(vd3);
            triangleStream.Append(vd4);

            triangleStream.RestartStrip();
        }

        static const float2 pixelSize = 1.0 / _ProjectionParams.xy;

        float median(float r, float g, float b)
        {
            return max(min(r, g), min(max(r, g), b));
        }

        float screenPxRange(float2 uv)
        {
            float2 unitRange = float2(_DistanceRange, _DistanceRange) / _AltasSize;
            float2 screenTexSize = rcp(fwidth(uv));
            return max(0.5 * dot(unitRange, screenTexSize), 1.0);
        }

        float4 frag(g2f i) : SV_Target
        {
            float3 msd = tex2D(_MainTex, i.uv).rgb;
            float sd = median(msd.r, msd.g, msd.b);
            float screenPxDist = screenPxRange(i.uv) * (sd - 0.5);
            float opacity = clamp(screenPxDist + 0.5, 0.0, 1.0);
            return float4(i.color, opacity);
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGB
            ZTest On // Off = Overlay
            ZWrite Off
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }

        /*Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest On
            ZWrite On
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }*/
    }
}