Shader "Hidden/ReGizmo/CopyFramebuffer"
{
    SubShader 
    {
        HLSLINCLUDE
        #pragma target 4.5
        #pragma only_renderers d3d11 playstation xboxone vulkan metal switch

        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/NormalBuffer.hlsl"

        struct Attributes
        {
            uint vertexID : SV_VertexID;
            UNITY_VERTEX_INPUT_INSTANCE_ID
        };

        struct Varyings
        {
            float4 positionCS : SV_POSITION; 
            float2 texcoord   : TEXCOORD0;
            UNITY_VERTEX_OUTPUT_STEREO
        };

        Varyings Vert(Attributes input)
        {
            Varyings output;

            UNITY_SETUP_INSTANCE_ID(input);
            UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

            output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID);
            output.texcoord = GetFullScreenTriangleTexCoord(input.vertexID);
            return output;
        }

        TEXTURE2D_X(_InputTexture);
        TEXTURE2D(_DepthTexture);
        ENDHLSL

        Pass
        {
            Name "Color"

            ZWrite Off
            ZTest Always
            Blend Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment color_frag

            float4 color_frag(Varyings input) : SV_TARGET
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                uint2 positionSS = input.texcoord * _ScreenSize.xy;
                float3 inColor = LOAD_TEXTURE2D_X(_InputTexture, positionSS).xyz;

                return float4(inColor, 1.0);
            }
            ENDHLSL
        }

        Pass
        {
            Name "Depth"

            ZWrite Off
            ZTest Always
            Blend Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment depth_frag

            float depth_frag(Varyings input) : SV_DEPTH
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                uint2 positionSS = input.texcoord * _ScreenSize.xy;
                return LoadCameraDepth(positionSS);
            }
            ENDHLSL
        }
    }

    Fallback Off
}