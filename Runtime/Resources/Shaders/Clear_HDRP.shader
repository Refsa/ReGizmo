Shader "Hidden/ReGizmo/HDRP_Clear"
{
    SubShader 
    {
        HLSLINCLUDE
        #pragma target 4.5
        #pragma only_renderers d3d11 playstation xboxone vulkan metal switch

        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/RenderPass/CustomPass/CustomPassCommon.hlsl"

        TEXTURE2D_X(_InputTexture);
        float4 _ClearColor;
        ENDHLSL

        Pass
        {
            Name "ClearColor"

            ZWrite Off
            ZTest Always
            Blend Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment color_frag

            float _Kek;

            float4 color_frag(Varyings input) : SV_TARGET
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                return _ClearColor;
            }
            ENDHLSL
        }
    }

    Fallback Off
}