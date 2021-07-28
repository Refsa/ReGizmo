Shader "Hidden/ReGizmo/Sprite"
{
    Properties { }
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
            uint vertexID : TEXCOORD0;
        };

        struct g2f
        {
            float4 pos : SV_POSITION;
            float2 uv: TEXCOORD0;
        };

        sampler2D _SpriteTexture;

        StructuredBuffer<SpriteProperties> _DrawData;

        v2g vert(uint vertexID : SV_VertexID)
        {
            v2g o;
            o.vertexID = vertexID;
            return o;
        }

        [maxvertexcount(4)]
        void geom(point v2g i[1], inout TriangleStream<g2f> triangleStream)
        {
            SpriteProperties bd = _DrawData[i[0].vertexID];
            float4 clip = mul(UNITY_MATRIX_VP, float4(bd.position, 1.0));

            float halfOffset = bd.scale * 0.5;
            float2 size = float2(-halfOffset, -halfOffset);

            size /= _ScreenParams.xy;
            size *= clip.w;

            if (ProjectionFlipped())
            {
                size.y = -size.y;
            }

            float4 cp1 = float4(clip.x - size.x, clip.y - size.y, clip.z, clip.w);
            float4 cp2 = float4(clip.x - size.x, clip.y + size.y, clip.z, clip.w);
            float4 cp3 = float4(clip.x + size.x, clip.y + size.y, clip.z, clip.w);
            float4 cp4 = float4(clip.x + size.x, clip.y - size.y, clip.z, clip.w);

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

            triangleStream.Append(g2);
            triangleStream.Append(g1);
            triangleStream.Append(g3);
            triangleStream.Append(g4);
            triangleStream.RestartStrip();
        }

        float4 frag(g2f i) : SV_Target
        {
            float4 tex_col = tex2D(_SpriteTexture, i.uv);
            clip(tex_col.a == 0 ? -1 : 1);
            return tex_col;
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest [_ZTest]
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
            ZTest LEqual
            ZWrite On

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment depth_frag
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            void depth_frag(g2f i, out float depth : SV_DEPTH)
            {
                float4 tex_col = tex2D(_SpriteTexture, i.uv);
                clip(tex_col.a == 0 ? -1 : 1);
                depth = i.pos.z;
            }
            ENDCG
        }
    }
}