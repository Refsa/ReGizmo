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
        

        struct v2g
        {
            uint vertexID : TEXCOORD0;
        };

        struct g2f
        {
            float4 pos   : SV_POSITION;
            float2 uv    : TEXCOORD0;
            float3 color : TEXCOORD1;
            float  z     : TEXCOORD2;
        };

        sampler2D _IconTexture;
        float _IconAspect;

        StructuredBuffer<IconProperties> _DrawData;

        v2g vert(uint vertexID : SV_VertexID)
        {
            v2g o;
            o.vertexID = vertexID;
            return o;
        }

        static const float aspect = _ScreenParams.x / _ScreenParams.y;

        [maxvertexcount(4)]
        void geom(point v2g i[1], inout TriangleStream<g2f> triangleStream)
        {
            IconProperties bd = _DrawData[i[0].vertexID];
            float4 clip = mul(UNITY_MATRIX_VP, float4(bd.position, 1.0));
            float z = abs(mul(UNITY_MATRIX_MV, float4(bd.position, 1.0)));

            float halfOffset = bd.scale;
            float2 size = float2(-halfOffset * _IconAspect, -halfOffset);

            // Scale the size to screen coords
            if (has_flag(bd.flags, SIZE_MODE_PIXEL))
            {
                size /= _ScreenParams.xy;
                size *= clip.w;
            }

            if (ProjectionFlipped())
            {
                size.y = -size.y;
            }

            // Create billboard vertices
            float4 cp1 = float4(clip.x - size.x, clip.y - size.y, clip.z, clip.w);
            float4 cp2 = float4(clip.x - size.x, clip.y + size.y, clip.z, clip.w);
            float4 cp3 = float4(clip.x + size.x, clip.y + size.y, clip.z, clip.w);
            float4 cp4 = float4(clip.x + size.x, clip.y - size.y, clip.z, clip.w);

            g2f g1;
            g1.pos = cp1;
            g1.uv = float2(1, 0);
            g1.color = bd.color;
            g1.z = z;

            g2f g2;
            g2.pos = cp2;
            g2.uv = float2(1, 1);
            g2.color = bd.color;
            g2.z = z;

            g2f g3;
            g3.pos = cp3;
            g3.uv = float2(0, 1);
            g3.color = bd.color;
            g3.z = z;

            g2f g4;
            g4.pos = cp4;
            g4.uv = float2(0, 0);
            g4.color = bd.color;
            g4.z = z;

            triangleStream.Append(g2);
            triangleStream.Append(g1);
            triangleStream.Append(g3);
            triangleStream.Append(g4);
            triangleStream.RestartStrip();
        }

        float4 _frag(g2f i)
        {
            float4 color = float4(i.color, 1.0);

            float4 tex_col = tex2D(_IconTexture, i.uv);
            color.rgb *= tex_col.a;
            color.a = tex_col.a;
            color.rgb *= tex_col;

            return color;
        }
        ENDCG

        Pass
        {
            Name "Render"

            Blend One One
            ZTest [_ZTest]
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float4 frag(g2f i) : SV_Target
            {
                float4 col = _frag(i);
                clip(col.a == 0 ? -1 : 1);
                return col;
            }
            ENDCG
        }

        Pass
        {
            Name "Depth"

            ZTest LEqual
            ZWrite On

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag_depth
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float frag_depth(g2f i, out float depth : SV_DEPTH) : SV_TARGET
            {
                float4 col = _frag(i);
                clip(col.a == 0 ? -1 : 1);
                
                depth = i.z;
                return depth;
            }
            ENDCG
        }

        Pass
        {
            Name "OIT_Revealage"
            ZWrite Off
            Blend Zero OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag_revealage
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float4 frag_revealage(g2f i) : SV_TARGET
            {
                float4 col = _frag(i);
                return col.aaaa;
            }
            ENDCG
        }
    }
}