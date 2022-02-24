Shader "Hidden/ReGizmo/Mesh_Wireframe"
{
    Properties
    {
    }
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
        
        struct vertex
        {
            float4 pos : POSITION;
            float3 normal : NORMAL;
        };

        struct v2g
        {
            float4 pos                : SV_POSITION;
            float dir                : TEXCOORD0;
            uint instanceID           : TEXCOORD1;
        };

        struct g2f
        {
            float4 pos   : SV_POSITION;
            float3 dist  : TEXCOORD0;
            float4 color : TEXCOORD1;
            float dir    : TEXCOORD2;
        };

        #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            StructuredBuffer<MeshProperties> _Properties;
        #endif

        void setup()
        {
        }

        v2g vert(vertex v, uint instanceID: SV_InstanceID)
        {
            v2g f = (v2g)0;
            f.instanceID = instanceID;

            #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
                MeshProperties prop = _Properties[instanceID];
                float4 worldPos = TRS(prop.Position, prop.Rotation, prop.Scale, v.pos);

                f.pos = UnityObjectToClipPos(worldPos);
                f.dir = dot(WorldSpaceViewDir(worldPos), rotate_vector(prop.Rotation, v.normal));
            #endif

            return f;
        }

        void adjust(inout float s0, inout float s1, inout float s2)
        {
            float M = max(s0, max(s1, s2));
            float m = min(s0, min(s1, s2));
            float fM = floor(M);
            float fm = floor(m);

            if ((fM - fm) == 1) {
                float range = M - m;
                s0 = (s0 - fM) / range + fM;
                s1 = (s1 - fM) / range + fM;
                s2 = (s2 - fM) / range + fM;
            }
        }

        [maxvertexcount(3)]
        void geom(triangle v2g i[3], inout TriangleStream<g2f> triangleStream)
        {
            float2 p0 = _ScreenParams.xy * i[0].pos.xy / i[0].pos.w;
            float2 p1 = _ScreenParams.xy * i[1].pos.xy / i[1].pos.w;
            float2 p2 = _ScreenParams.xy * i[2].pos.xy / i[2].pos.w;

            float2 edge0 = p2 - p1;
            float2 edge1 = p2 - p0;
            float2 edge2 = p1 - p0;

            float area = abs(edge1.x * edge2.y - edge1.y * edge2.x);

            #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
                float4 color = _Properties[i[0].instanceID].Color;
            #else
                float4 color = float4(1, 0, 1, 1);
            #endif

            g2f o;
            o.color = color;

            float d0 = area / length(edge0);
            float d1 = area / length(edge1);
            float d2 = area / length(edge2);
            // adjust(d0, d1, d2);

            o.pos = i[0].pos;
            o.dist = float3(d0, 0, 0);
            o.dir = i[0].dir;
            triangleStream.Append(o);

            o.pos = i[1].pos;
            o.dist = float3(0, d1, 0);
            o.dir = i[1].dir;
            triangleStream.Append(o);

            o.pos = i[2].pos;
            o.dist = float3(0, 0, d2);
            o.dir = i[2].dir;
            triangleStream.Append(o);
        }

        float4 _frag(g2f i)
        {
            const float thickness = 0.5;
            const float firmness = 1;

            float d =  min(i.dist[0], min(i.dist[1], i.dist[2]));
            d = exp2(-1 / thickness * d * d);

            float4 fillColor = float4(i.color.rgb, 0);
            float4 wireColor = float4(i.color.rgb, 1);

            float color_scale = i.dir < 0 ? 0.33 : 1.0;
            return lerp(fillColor, wireColor * color_scale, d);
        }
        ENDCG

        Pass
        {
            Name "Render"
            Blend One One
            ZTest [_ZTest]
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float4 frag(g2f i) : SV_Target
            {
                float4 color = _frag(i);
                return color;
            }
            ENDCG
        }

        Pass
        {
            Name "Depth"
            ZTest LEqual
            ZWrite On
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment depth_frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            void depth_frag(g2f i, out float depth : SV_DEPTH)
            {
                float4 color = _frag(i);
                clip(color.a <= 0.0001 ? -1 : 1);
                depth = i.pos.z;
            }
            ENDCG
        }

        Pass
        {
            Name "OIT_Revealage"
            Blend Zero OneMinusSrcAlpha
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment revealage_frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float4 revealage_frag(g2f i) : SV_TARGET
            {
                float4 color = _frag(i);
                return color.aaaa;
            }
            ENDCG
        }

        Pass
        {
            Name "RenderFront"
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest LEqual
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float4 frag(g2f i) : SV_Target
            {
                float4 color = _frag(i);
                return color;
            }
            ENDCG
        }

        Pass
        {
            Name "RenderBehind"
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest Greater
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float4 frag(g2f i) : SV_Target
            {
                float4 color = _frag(i);
                color.a *= _AlphaBehindScale;
                return color;
            }
            ENDCG
        }
    }
}