Shader "ReGizmo/FontShader" {
    Properties {
        _MainTex ("Font Texture", 2D) = "white" {}
    }

    SubShader {

        Tags {
            "Queue"="Transparent+1000"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
        }
        Lighting Off Cull Off
        Blend SrcAlpha OneMinusSrcAlpha

        CGINCLUDE
        #include "UnityCG.cginc"
        struct appdata_t {
            float4 vertex : POSITION;
            float4 color : COLOR;
            float2 texcoord : TEXCOORD0;
        };

        struct v2g {
            float4 vertex : SV_POSITION;
            uint vertexID : TEXCOORD0;
        };

        struct g2f {
            float4 pos: SV_POSITION;
            float2 uv: TEXCOORD0;
            float3 color: TEXCOORD1;
            uint vertexID: TEXCOORD4;
        };

        struct CharacterInfo {
            float2 BottomLeft;
            float2 BottomRight;
            float2 TopLeft;
            float2 TopRight;
            float4 Size;
            float Advance;
        };

        struct CharData { 
            float3 Position;
            float Scale;
            uint CharIndex;
            float3 Color;
            float Advance;
        };

        StructuredBuffer<CharData> _CharData;
        StructuredBuffer<CharacterInfo> _CharacterInfos;

        sampler2D _MainTex;
        uniform float4 _MainTex_ST;

        v2g vert (appdata_t v, uint vid : SV_VertexID)
        {
            CharData cd = _CharData[vid];

            v2g o;
            o.vertex = float4(cd.Position, 1.0);
            o.vertexID = vid;

            return o;
        }

        [maxvertexcount(6)]
        void geom(point v2g i[1], inout TriangleStream<g2f> triangleStream)
        {
            uint vid = i[0].vertexID;

            CharData cd = _CharData[vid];
            CharacterInfo ci = _CharacterInfos[cd.CharIndex];

            float4 center = i[0].vertex;
            float4 advanceOffset = float4(cd.Advance, 0, 0, 0);

            float4 centerClip = UnityObjectToClipPos(center);

            float4 c1 = float4(ci.Size.x, -ci.Size.w, 0, 0) * cd.Scale + advanceOffset;
            float4 c2 = float4(ci.Size.y, -ci.Size.w, 0, 0) * cd.Scale + advanceOffset;
            float4 c3 = float4(ci.Size.y, ci.Size.z, 0, 0) * cd.Scale + advanceOffset;
            float4 c4 = float4(ci.Size.x, ci.Size.z, 0, 0) * cd.Scale + advanceOffset;

            float4 p1 = centerClip + c4;
            float4 p2 = centerClip + c1;
            float4 p3 = centerClip + c2;
            float4 p4 = centerClip + c3;

            g2f vd1;
            vd1.pos = (p1);
            vd1.uv = ci.BottomLeft;
            vd1.vertexID = i[0].vertexID;
            vd1.color = cd.Color;

            g2f vd2;
            vd2.pos = (p2);
            vd2.uv = ci.TopLeft;
            vd2.vertexID = i[0].vertexID;
            vd2.color = cd.Color;

            g2f vd3;
            vd3.pos = (p3);
            vd3.uv = ci.TopRight;
            vd3.vertexID = i[0].vertexID;
            vd3.color = cd.Color;

            g2f vd4; 
            vd4.pos = (p4);
            vd4.uv = ci.BottomRight;
            vd4.vertexID = i[0].vertexID;
            vd4.color = cd.Color;

            triangleStream.Append(vd1);
            triangleStream.Append(vd2);
            triangleStream.Append(vd3);

            triangleStream.Append(vd1);
            triangleStream.Append(vd3);
            triangleStream.Append(vd4);
        }

        float4 frag (g2f i) : SV_Target
        {
            float4 col = float4(i.color, 1.0);
            float4 tex = tex2D(_MainTex, i.uv);
            col *= tex.a;

            // Good for pixelated text
            //clip(tex.a == 0 ? -1 : 1);

            // To debug the quad the letter is drawn on
            // if (tex.a == 0) col = float4(1,0,1,1);

            return col;
        }
        ENDCG

        Pass {
            ZTest Greater ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }

        Pass {
            ZTest LEqual ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }
    }
}