Shader "Hidden/ReGizmo/Icon"
{
    Properties { }
    SubShader
    {
        Tags
        {
            "RenderType"="Overlay" 
            "Queue"="Overlay" 
        }

        CGINCLUDE
        #include "Utils/ReGizmoShaderUtils.cginc"
        #pragma target 4.6

        struct v2g
        {
            float4 pos : SV_POSITION;
            uint vertexID : TEXCOORD0;
        };

        struct g2f
        {
            float4 pos : SV_POSITION;
            float2 uv: TEXCOORD0;
            float3 color: TEXCOORD1;
        };

        struct DrawData
        {
            float3 position;
            float3 color;
            float scale;
        };

        sampler2D _IconTexture;
        float _IconAspect;

        StructuredBuffer<DrawData> _DrawData;

        v2g vert(uint vertexID : SV_VertexID)
        {
            DrawData data = _DrawData[vertexID];

            v2g o;

            o.pos = float4(data.position, 1.0);
            o.vertexID = vertexID;

            return o;
        }

        static const float aspect = _ScreenParams.x / _ScreenParams.y;

        [maxvertexcount(4)]
        void geom(point v2g i[1], inout TriangleStream<g2f> triangleStream)
        {
            DrawData bd = _DrawData[i[0].vertexID];

            float halfOffset = bd.scale * 0.5;

            float4 clip = mul(UNITY_MATRIX_VP, i[0].pos);

            float dx = -halfOffset * _IconAspect;
            float dy = -halfOffset * aspect;

            if (unity_OrthoParams.w == 1.0)
            {
                // TODO: get rid of magic number
                dx = (dx / unity_OrthoParams.x) * 0.01;
                dy = (dy / unity_OrthoParams.x) * 0.01;
            }

            if (ProjectionFlipped())
            {
                dy = -dy;
            }

            float4 cp1 = float4(clip.x - dx, clip.y - dy, clip.z, clip.w);
            float4 cp2 = float4(clip.x - dx, clip.y + dy, clip.z, clip.w);
            float4 cp3 = float4(clip.x + dx, clip.y + dy, clip.z, clip.w);
            float4 cp4 = float4(clip.x + dx, clip.y - dy, clip.z, clip.w);

            g2f g1;
            g1.pos = cp1;
            g1.uv = float2(1, 0);
            g1.color = bd.color;

            g2f g2;
            g2.pos = cp2;
            g2.uv = float2(1, 1);
            g2.color = bd.color;

            g2f g3;
            g3.pos = cp3;
            g3.uv = float2(0, 1);
            g3.color = bd.color;

            g2f g4;
            g4.pos = cp4;
            g4.uv = float2(0, 0);
            g4.color = bd.color;

            triangleStream.Append(g2);
            triangleStream.Append(g1);
            triangleStream.Append(g3);
            triangleStream.Append(g4);
            triangleStream.RestartStrip();
        }

        float4 frag(g2f i) : SV_Target
        {
            float4 color = float4(i.color, 1.0);

            float4 tex_col = tex2D(_IconTexture, i.uv);
            color *= tex_col.a;

            return lerp(tex_col, color, 0.5);
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest LEqual
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
            ZTest LEqual
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