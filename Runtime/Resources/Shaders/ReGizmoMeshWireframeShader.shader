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

            o.pos = i[0].pos;
            o.dist = float3(area / length(edge0), 0, 0);
            o.dir = i[0].dir;
            triangleStream.Append(o);

            o.pos = i[1].pos;
            o.dist = float3(0, area / length(edge1), 0);
            o.dir = i[1].dir;
            triangleStream.Append(o);

            o.pos = i[2].pos;
            o.dist = float3(0, 0, area / length(edge2));
            o.dir = i[2].dir;
            triangleStream.Append(o);
        }

        float4 _frag(g2f i)
        {
            float d = min(i.dist[0], min(i.dist[1], i.dist[2]));
            float I = exp2(-0.75 * d * d * d * d);

            float4 fillColor = float4(i.color.rgb, 0);
            float4 wireColor = float4(i.color.rgb, 1);

            float color_scale = i.dir < -0.5 ? 0.33 : 1.0;
            return lerp(fillColor, wireColor * color_scale, I);
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
            Cull Back

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
                clip(color.a == 0 ? -1 : 1);
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
            Blend Zero OneMinusSrcAlpha
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
                // return i.color;

                float4 color = _frag(i);
                return color;
            }
            ENDCG
        }

        Pass
        {
            Name "RenderBehind"
            Blend Zero OneMinusSrcAlpha
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