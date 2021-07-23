Shader "Hidden/ReGizmo/CopyFramebuffer"
{
    SubShader 
    {
        HLSLINCLUDE
        #pragma target 4.5
        #pragma only_renderers d3d11 playstation xboxone vulkan metal switch

        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/RenderPass/CustomPass/CustomPassCommon.hlsl"

        TEXTURE2D_X(_InputTexture);
        TEXTURE2D(_DepthTexture);

        struct HVaryings
        {
            float4 positionCS : SV_POSITION;
            float2 texcoord   : TEXCOORD0;
            UNITY_VERTEX_OUTPUT_STEREO
        };

        HVaryings HVert(Attributes input)
        {
            HVaryings output;
            UNITY_SETUP_INSTANCE_ID(input);
            UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
            output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID, UNITY_RAW_FAR_CLIP_VALUE);
            output.texcoord = GetFullScreenTriangleTexCoord(input.vertexID);
            return output;
        }
        ENDHLSL

        Pass
        {
            Name "Color"

            ZWrite Off
            ZTest Always
            Blend Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex HVert
            #pragma fragment color_frag

            float _Kek;

            float4 color_frag(HVaryings input) : SV_TARGET
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                float depth = LoadCameraDepth(input.positionCS.xy);
                PositionInputs posInput = GetPositionInput(input.positionCS.xy, _ScreenSize.zw, depth, UNITY_MATRIX_I_VP, UNITY_MATRIX_V);

                float2 uv = posInput.positionNDC.xy * _RTHandleScale.xy;
                float3 inColor = SAMPLE_TEXTURE2D_X_LOD(_InputTexture, s_linear_clamp_sampler, uv, 0);

                if (_Kek == 1.0)
                {
                    inColor = 1 - inColor;
                }

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
            #pragma vertex HVert
            #pragma fragment depth_frag

            float depth_frag(HVaryings input) : SV_DEPTH
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                // float depth = LoadCameraDepth(input.positionCS.xy);
                // return depth;

                uint2 positionSS = input.texcoord * _ScreenSize.xy;
                return LoadCameraDepth(positionSS);
            }
            ENDHLSL
        }
    }

    Fallback Off
}