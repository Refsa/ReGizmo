Shader "Hidden/ReGizmo/Texture"
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
            float4 uv  : TEXCOORD;
        };

        struct v2f
        {
            float4 pos : SV_POSITION;
            float2 uv  : TEXCOORD0;
            float4 col : TEXCOORD1;
        };

        #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            StructuredBuffer<MeshProperties> _Properties;
        #endif

        sampler2D _Texture;
        float _Aspect;

        void setup() { }

        v2f vert(vertex v, uint instanceID: SV_InstanceID)
        {
            v2f f = (v2f)0;
            f.uv = v.uv;
            #if UNITY_UV_STARTS_AT_TOP
            f.uv.y = 1 - f.uv.y;
            #endif

            #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
                MeshProperties prop = _Properties[instanceID]; 
                float4 cloc = TRS(prop.Position, prop.Rotation, prop.Scale, v.pos);
                f.pos = mul(UNITY_MATRIX_VP, cloc);
                f.col = prop.Color; 
            #endif

            return f;
        }
        ENDCG

        Pass
        {
            Name "RenderOIT"

            Blend One One
            ZTest [_ZTest]
            ZWrite Off
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float4 frag(v2f f) : SV_Target
            {
                float4 col = f.col;
                return col;
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
            #pragma vertex depth_vert
            #pragma fragment depth_frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

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

            void depth_frag(v2f_depth i, out float depth : SV_DEPTH)
            {
                depth = i.pos.z;
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
            #pragma fragment revealage_frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float4 revealage_frag(v2f i) : SV_TARGET
            {
                return i.col.aaaa;
            }

            ENDCG
        }

        Pass
        {
            Name "RenderFront"

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

            float4 frag(v2f f) : SV_Target
            {
                float4 col = f.col;
                float4 tex_col = tex2D(_Texture, f.uv);
                col.rgb *= tex_col.rgb * tex_col.a;
                col.a = tex_col.a;

                return col;
            }
            ENDCG
        }

        Pass
        {
            Name "RenderBehind"

            Blend SrcAlpha OneMinusSrcAlpha
            ZTest Greater
            ZWrite Off
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON

            float4 frag(v2f f) : SV_Target
            {
                float4 col = f.col;
                float4 tex_col = tex2D(_Texture, f.uv);
                col.rgb *= tex_col.rgb * tex_col.a;
                col.a = tex_col.a;

                return col;
            }
            ENDCG
        }
    }
}