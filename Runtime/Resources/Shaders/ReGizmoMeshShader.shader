Shader "Hidden/ReGizmo/Mesh"
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
            float4 pos    : POSITION;
            float3 normal : NORMAL;
        };

        struct v2f
        {
            float4 pos      : SV_POSITION;
            float4 col      : TEXCOORD0;
            float  strength : TEXCOORD1;
        };

        #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            StructuredBuffer<MeshProperties> _Properties;
        #endif
        float _Shaded;
        float _FresnelFactor;

        void setup() { }

        v2f vert(vertex v, uint instanceID: SV_InstanceID)
        {
            v2f f = (v2f)0;

            #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
                MeshProperties prop = _Properties[instanceID]; 
                float4 cloc = TRS(prop.Position, prop.Rotation, prop.Scale, v.pos);
                f.pos = mul(UNITY_MATRIX_VP, cloc);
                f.col = prop.Color; 

                float3 normal = rotate_vector(prop.Rotation, normalize(UnityObjectToWorldNormal(v.normal)));
                float3 viewDir = normalize(WorldSpaceViewDir(cloc));
                f.strength = pow(abs(dot(normal, viewDir)), _FresnelFactor * 2);
            #endif

            return f;
        }

        float4 frag(v2f f) : SV_Target
        {
            float4 c = f.col;
            float3 shade = lerp(c.rgb, 0, _Shaded);
            c.rgb = saturate(lerp(shade, c.rgb, f.strength));

            return c;
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest LEqual
            ZWrite Off
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }

        Pass
        {
            ZTest LEqual
            Cull Back
            ZWrite On

            CGPROGRAM
            struct v2f_depth
            {
                float4 pos: SV_POSITION;
            };

            v2f_depth depth_vert(vertex i, uint instanceID: SV_INSTANCEID)
            {
                v2f_depth o = (v2f_depth) 0;

                #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
                    MeshProperties prop = _Properties[instanceID]; 
                    float4 cloc = TRS(prop.Position, prop.Rotation, prop.Scale, i.pos);
                    o.pos = mul(UNITY_MATRIX_VP, cloc);
                #endif

                return o;
            }

            float depth_frag(v2f_depth i) : SV_TARGET1
            {
                return i.pos.z;
            }

            #pragma vertex depth_vert
            #pragma fragment depth_frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }
    }
}