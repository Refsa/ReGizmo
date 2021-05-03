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
        Blend SrcAlpha OneMinusSrcAlpha

        CGINCLUDE
        #include "UnityCG.cginc"
        #include "Utils/ReGizmoShaderUtils.cginc"

        struct vertex
        {
            float4 loc : POSITION;
        };

        struct fragment
        {
            float4 loc : SV_POSITION;
            float4 worldSpacePosition : TEXCOORD0;
            uint instanceID: TEXCOORD1;
        };

        struct g2f
        {
            float4 loc : SV_POSITION;
            float3 dist : TEXCOORD0;
            float4 color: TEXCOORD2;
        };

        struct Properties
        {
            float3 Position;
            float3 Rotation;
            float3 Scale;
            float4 Color;
        };

        #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
        StructuredBuffer<Properties> _Properties;
        #endif

        void setup()
        {
        }

        fragment vert(vertex v, uint instanceID: SV_InstanceID)
        {
            fragment f;

            #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            Properties prop = _Properties[instanceID];
            f.worldSpacePosition = TRS(prop.Position, prop.Rotation, prop.Scale, v.loc);
            #else
            f.worldSpacePosition = mul(unity_ObjectToWorld, v.loc);
            #endif

            f.loc = UnityObjectToClipPos(f.worldSpacePosition);
            f.instanceID = instanceID;

            return f;
        }

        [maxvertexcount(3)]
        void geom(triangle fragment i[3], inout TriangleStream<g2f> triangleStream)
        {
            float2 p0 = _ScreenParams.xy * i[0].loc.xy / i[0].loc.w;
            float2 p1 = _ScreenParams.xy * i[1].loc.xy / i[1].loc.w;
            float2 p2 = _ScreenParams.xy * i[2].loc.xy / i[2].loc.w;

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

            o.loc = i[0].loc;
            o.dist = float3(area / length(edge0), 0, 0);
            triangleStream.Append(o);

            o.loc = i[1].loc;
            o.dist = float3(0, area / length(edge1), 0);
            triangleStream.Append(o);

            o.loc = i[2].loc;
            o.dist = float3(0, 0, area / length(edge2));
            triangleStream.Append(o);
        }

        float4 frag(g2f i) : SV_Target
        {
            float d = min(i.dist[0], min(i.dist[1], i.dist[2]));
            float I = exp2(-1.4 * d * d);

            float4 fillColor = float4(i.color.rgb, 0);
            float4 wireColor = float4(i.color.rgb, 1);
            return lerp(fillColor, wireColor, I);
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGB
            ZTest On
            ZWrite On

            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }
    }
}