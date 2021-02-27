Shader "ReGizmo/Line"
{
    Properties {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 0, 1, 1)
    }
    SubShader {
        Tags {
            "RenderType" = "Transparent"
            "IgnoreProjector" = "True"
            "Queue" = "Transparent+10"
        }

        Cull Off
        Lighting Off
        ZTest Always
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"

            struct vertex {
                float4 loc  : POSITION;
                float2 uv   : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            struct fragment {
                float4 loc  : SV_POSITION;
                float2 uv   : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            sampler2D _MainTex;
            
            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
            UNITY_INSTANCING_BUFFER_END(Props)
            
            fragment vert(vertex v) {
                fragment f;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, f);

                f.loc = UnityObjectToClipPos(v.loc);
                f.uv = v.uv;
                return f;
            }

            float4 frag(fragment f) : SV_Target{
                UNITY_SETUP_INSTANCE_ID(f);

                float4 col = UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
                // float4 c = tex2D(_MainTex, f.uv);
                return col;
            }
            ENDCG
        }
    }
}
