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

        struct vertex
        {
            float4 pos : POSITION;
            float3 normal : NORMAL;
        };

        struct v2f
        {
            float4 pos       : SV_POSITION;
            float3 col       : TEXCOORD0;
            float3 world_pos : TEXCOORD1;
            float3 center    : TEXCOORD2;
            float  range     : TEXCOORD3;
        };

        struct GridProperties
        {
            float3 Position;
            float Range;
            float3 Normal;
            float3 Color;
        };

    #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
        StructuredBuffer<GridProperties> _Properties;
    #endif

        void setup()
        {
        }

        v2f vert(vertex v, uint instanceID: SV_InstanceID)
        {
            v2f f = (v2f)0;

        #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            GridProperties prop = _Properties[instanceID]; 
            float3 offset = _WorldSpaceCameraPos * float3(1,0,1) * dot(prop.Normal, prop.Normal);
            float3 center = prop.Position + offset;

            float4 cloc = TRS(
                center,
                rotate_angle_axis(radians(90), float3(1,0,0)), 
                prop.Range, 
                v.pos);

            f.pos = mul(UNITY_MATRIX_VP, cloc);
            f.col = prop.Color;
            f.world_pos = cloc.xyz;
            f.center = center;
            f.range = prop.Range;
        #endif

            return f;
        }

        float4 grid(float3 world_pos, float scale, float3 color)
        {
            float2 coord = world_pos.xz * scale;
            float2 derivative = fwidth(coord);
            float2 grid = abs(frac(coord - 0.5) - 0.5) / derivative;
            float l = min(grid.x, grid.y);

            float4 _color = float4(color, 1.0 - min(l, 1.0));

            return _color;
        }

        float4 frag(v2f i) : SV_Target
        {
            float4 small_grid = grid(i.world_pos, 1, float3(0.2,0.2,0.2));
            float4 large_grid = grid(i.world_pos, 0.1, float3(0.5,0.5,0.5));

            float4 grid = small_grid;
            grid.rgb += large_grid * large_grid.a;
            grid.a += large_grid.a;

            float dist = 1 - saturate(distance(i.center, i.world_pos) / (i.range * 0.9));
            dist = pow(dist, 5);
            grid.a *= dist;

            float depth = Linear01Depth(i.pos.z / i.pos.w);
            grid.a *= (1 - pow(depth, 2.5));

            clip(grid.a == 0 ? -1 : 1);

            return grid;
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest LEqual
            ZWrite On
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }
    }
}