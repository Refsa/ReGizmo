Shader "Hidden/ReGizmo/Grid"
{
    Properties { }
    SubShader
    {
        Tags
        {
            "RenderType" = "Overlay"
            "Queue" = "Overlay"
        }

        CGINCLUDE
        #include "UnityCG.cginc"
        #include "Utils/ReGizmoShaderUtils.cginc"

        struct v2g_line
        {
            uint vertexID: TEXCOORD0;
        };

        struct g2f_line
        {
            float4 pos: SV_POSITION;
            noperspective float2 uv : TEXCOORD0;
            nointerpolation float3 color: TEXCOORD1;
            nointerpolation float width: TEXCOORD2;
        };

        struct GridProperties
        {
            float3 Position1;
            float3 Position2;
            float3 Color;
            float Width;
        };

        StructuredBuffer<GridProperties> _Properties;

        v2g_line vert_line(uint vertexID: SV_VertexID)
        {
            v2g_line f = (v2g_line)0;
            f.vertexID = vertexID;
            return f;
        }

        [maxvertexcount(4)]
        void geom_line(point v2g_line i[1], inout TriangleStream<g2f_line> triangleStream)
        {
            GridProperties props = _Properties[i[0].vertexID];

            float3 offset1 = float3(_WorldSpaceCameraPos.x, 0.0, _WorldSpaceCameraPos.z);
            float3 offset2 = float3(_WorldSpaceCameraPos.x, 0.0, _WorldSpaceCameraPos.z);
            offset1 -= offset1 % 10.0;
            offset2 -= offset2 % 10.0;

            float3 pos1 = props.Position1 + offset1;
            float3 pos2 = props.Position2 + offset2;

        
            float4 p1 = UnityObjectToClipPos(float4(pos1, 1.0));
            float4 p2 = UnityObjectToClipPos(float4(pos2, 1.0));
        
            // Sort by point "closest" to screen-space
            if (p1.w > p2.w)
            {
                float4 clip_temp = p1;
                p1 = p2;
                p2 = clip_temp;

                float3 pos_temp = pos1;
                pos1 = pos2;
                pos2 = pos_temp;
            }
            
            // Manual near-clip
            if (p1.w < _ProjectionParams.y)
            {
                float ratio = (_ProjectionParams.y - p1.w) / (p2.w - p1.w);
                p1 = lerp(p1, p2, ratio);
            }

            float w1 = ceil(props.Width + PixelSize);
            float w2 = ceil(props.Width + PixelSize);
        
            float2 a = p1.xy / p1.w;
            float2 b = p2.xy / p2.w;
            float2 c1 = normalize(float2(a.y - b.y, b.x - a.x)) / _ScreenParams.xy * w1;
            float2 c2 = normalize(float2(a.y - b.y, b.x - a.x)) / _ScreenParams.xy * w2;
        
            g2f_line g0 = (g2f_line)0;
            g2f_line g1 = (g2f_line)0;
            g2f_line g2 = (g2f_line)0;
            g2f_line g3 = (g2f_line)0;
        
            g0.pos = float4(p1.xy + c1 * p1.w, p1.zw);
            g1.pos = float4(p1.xy - c1 * p1.w, p1.zw);
            g2.pos = float4(p2.xy + c2 * p2.w, p2.zw);
            g3.pos = float4(p2.xy - c2 * p2.w, p2.zw);
        
            g0.uv = float2(0, 0);
            g1.uv = float2(1, 0);
            g2.uv = float2(0, 1);
            g3.uv = float2(1, 1);
        
            g0.color = props.Color;
            g1.color = props.Color;
            g2.color = props.Color;
            g3.color = props.Color;
        
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
        
        float4 frag_line(g2f_line g) : SV_Target
        {
            float4 col = float4(g.color, 0.0);
        
            // TODO: Unoptimal since we can avoid this distance check, but works for now
            const float2 center_uv = float2(0.5, g.uv.y);
            const float dist = distance(g.uv, center_uv) * 2;
        
            static const float width_factor = 0.2;
            static const float sharpness = 2.7;
            static const float smoothing = 3.5;
        
            float x = pow(dist, g.width * width_factor);
            col.a = exp2(-smoothing * pow(x, sharpness));

            float depth = 1 - Linear01Depth(g.pos.z / g.pos.w);
            depth = 1 - exp2(-_ProjectionParams.z * depth * depth);
            col.a *= depth;

            clip(col.a == 0 ? -1 : 1);

            return col;
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            ZTest LEqual

            CGPROGRAM
            #pragma vertex vert_line
            #pragma geometry geom_line
            #pragma fragment frag_line
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }
    }
}