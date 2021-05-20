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
            float3 center : TEXCOORD3;
            float3 world_pos : TEXCOORD4;
            nointerpolation float range : TEXCOORD5;
        };

        struct GridProperties
        {
            float3 Position1;
            float3 Position2;
            uint ID;
            uint Index;
        };

        struct GridMeta
        {
            float Range;
            float Width;
            float3 Normal;
            float3 Color;
            float3 Center;
        };

        StructuredBuffer<GridProperties> _Properties;
        StructuredBuffer<GridMeta> _MetaData;

        v2g_line vert_line(uint vertexID: SV_VertexID)
        {
            v2g_line f = (v2g_line)0;
            f.vertexID = vertexID;
            return f;
        }

        [maxvertexcount(4)]
        void geom_grid(point v2g_line i[1], inout TriangleStream<g2f_line> triangleStream)
        {
            GridProperties props = _Properties[i[0].vertexID];
            GridMeta meta = _MetaData[props.ID];

            float3 offset_mul = 1 - meta.Normal;
            float3 offset1 = _WorldSpaceCameraPos.xyz * offset_mul;
            float3 offset2 = _WorldSpaceCameraPos.xyz * offset_mul;
            offset1 -= offset1 % 10.0;
            offset2 -= offset2 % 10.0;
            float use_offset = (dot(meta.Center, meta.Center) == 0);

            float3 pos1 = props.Position1 + offset1 * use_offset;
            float3 pos2 = props.Position2 + offset2 * use_offset;
        
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

            float3 center = meta.Center + offset1 * use_offset;

            float w1 = ceil(meta.Width + PixelSize);
            float w2 = ceil(meta.Width + PixelSize);
        
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

            float color_mul = props.Index % 10 == 0 ? 1.0 : 0.5;
        
            g0.color = meta.Color * color_mul;
            g1.color = meta.Color * color_mul;
            g2.color = meta.Color * color_mul;
            g3.color = meta.Color * color_mul;
        
            g0.width = w1;
            g1.width = w1;
            g2.width = w2;
            g3.width = w2;

            g0.center = center;
            g1.center = center;
            g3.center = center;
            g2.center = center;

            float3 wp1 = pos1;
            float3 wp2 = pos2;

            g0.world_pos = wp1;
            g1.world_pos = wp1;
            g2.world_pos = wp2;
            g3.world_pos = wp2;

            g0.range = meta.Range;
            g1.range = meta.Range;
            g2.range = meta.Range;
            g3.range = meta.Range;

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
        
        float4 frag_grid(g2f_line g) : SV_Target
        {
            float4 col = float4(g.color, 0.0);
        
            // TODO: Unoptimal since we can avoid this distance check, but works for now
            const float2 center_uv = float2(0.5, g.uv.y);
            const float dist = distance(g.uv, center_uv) * 2;
        
            static const float width_factor = 0.2;
            static const float sharpness = 2.5;
            static const float smoothing = 3.5;
        
            float x = pow(dist, g.width * width_factor);
            col.a = exp2(-smoothing * pow(x, sharpness));

            //float range = min(g.range, _ProjectionParams.z * 0.5);
            float range = g.range;
            float center_dist = 1 - saturate(distance(g.world_pos, g.center) / range);
            center_dist = pow(center_dist, 1);
            col.a *= center_dist;

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
            #pragma geometry geom_grid
            #pragma fragment frag_grid
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }
    }
}