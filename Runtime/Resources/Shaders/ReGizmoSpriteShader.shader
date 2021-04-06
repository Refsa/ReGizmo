Shader "Hidden/ReGizmo/Sprite"
{
    Properties
    {
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Overlay" "Queue"="Overlay"
        }

        CGINCLUDE
        #include "Utils/ReGizmoShaderUtils.cginc"

        struct v2g
        {
            float4 pos : SV_POSITION;
            uint vertexID : TEXCOORD0;
        };

        struct g2f
        {
            float4 pos : SV_POSITION;
            float2 uv: TEXCOORD0;
        };

        struct Data
        {
            float3 position;
            float scale;
            // xMin, yMin, xMax, yMax
            float4 uvs;
        };

        sampler2D _SpriteTexture;
        float4 _SpriteTexture_TexelSize;

        StructuredBuffer<Data> _Properties;

        v2g vert(uint vertexID : SV_VertexID)
        {
            Data data = _Properties[vertexID];

            v2g o;

            o.pos = float4(data.position, 1.0);
            o.vertexID = vertexID;

            return o;
        }

        static const float aspect = _ScreenParams.x / _ScreenParams.y;

        [maxvertexcount(6)]
        void geom(point v2g i[1], inout TriangleStream<g2f> triangleStream)
        {
            Data bd = _Properties[i[0].vertexID];

            float halfOffset = bd.scale * 0.5;

            float dx = -halfOffset;
            float dy = -halfOffset * aspect;

            float4 clip = 0;
            if (unity_OrthoParams.w == 1.0)
            {
                dx = (dx / unity_OrthoParams.x) * rcp(bd.scale);
                dy = (dy / unity_OrthoParams.x) * rcp(bd.scale);

                clip = mul(UNITY_MATRIX_VP, i[0].pos) + float4(-dx, dy, 0, 0);
            }
            else
            {
                clip = mul(UNITY_MATRIX_VP, i[0].pos);
            }

            float4 cp1 = float4(clip.x - dx, clip.y - dy, clip.z, clip.w);
            float4 cp2 = float4(clip.x - dx, clip.y + dy, clip.z, clip.w);
            float4 cp3 = float4(clip.x + dx, clip.y + dy, clip.z, clip.w);
            float4 cp4 = float4(clip.x + dx, clip.y - dy, clip.z, clip.w);

            g2f g1;
            g1.pos = cp1;
            g1.uv = float2(bd.uvs.z, bd.uvs.y);

            g2f g2;
            g2.pos = cp2;
            g2.uv = float2(bd.uvs.z, bd.uvs.w);

            g2f g3;
            g3.pos = cp3;
            g3.uv = float2(bd.uvs.x, bd.uvs.w);

            g2f g4;
            g4.pos = cp4;
            g4.uv = float2(bd.uvs.x, bd.uvs.y);

            triangleStream.Append(g1);
            triangleStream.Append(g2);
            triangleStream.Append(g3);
            triangleStream.Append(g1);
            triangleStream.Append(g4);
            triangleStream.Append(g3);
            triangleStream.RestartStrip();
        }

        float4 frag(g2f i) : SV_Target
        {
            float4 tex_col = tex2D(_SpriteTexture, i.uv);

            return tex_col;
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGB
            ZTest On
            ZWrite Off

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
            ZTest On
            ZWrite On

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