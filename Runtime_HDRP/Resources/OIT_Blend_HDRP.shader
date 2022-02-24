Shader "Hidden/OIT/HDRP_Blend" {
    Properties {
        _BackgroundTex ("Main Tex", 2D) = "white" {}
        _AccumTex ("Accum", 2D) = "black" {}
        _RevealageTex ("Revealage", 2D) = "white" {}
    } 

    HLSLINCLUDE
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/RenderPass/CustomPass/CustomPassCommon.hlsl"
    #include "OIT_Blend_Shared.cginc"

    TEXTURE2D_X(_BackgroundTex);
    TEXTURE2D_X(_AccumTex);
    TEXTURE2D_X(_RevealageTex);

    float4 frag(Varyings input) : SV_Target 
    {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

        float depth = LoadCameraDepth(input.positionCS.xy);
        PositionInputs posInput = GetPositionInput(input.positionCS.xy, _ScreenSize.zw, depth, UNITY_MATRIX_I_VP, UNITY_MATRIX_V);
        float2 uv = posInput.positionNDC.xy * _RTHandleScale.xy;

        float4 background = SAMPLE_TEXTURE2D_X_LOD(_BackgroundTex, s_linear_clamp_sampler, uv, 0);
        float4 accum = SAMPLE_TEXTURE2D_X_LOD(_AccumTex, s_linear_clamp_sampler, posInput.positionNDC.xy, 0);
        float revealage = SAMPLE_TEXTURE2D_X_LOD(_RevealageTex, s_linear_clamp_sampler, posInput.positionNDC.xy, 0).x;

        return oit_blend(background, accum, revealage);
    }
    ENDHLSL

    SubShader { 
        ZTest Always Cull Off ZWrite Off
        
        Pass {
            HLSLPROGRAM
            #pragma target 4.5
            #pragma only_renderers d3d11 playstation xboxone vulkan metal switch
            #pragma vertex Vert
            #pragma fragment frag
            ENDHLSL
        }
    } 
}