Shader "Hidden/ReGizmo/TriangleShader"
{
    Properties { }
    SubShader
    {
        Tags { "RenderType"="Overlay" "Queue"="Overlay" }

        CGINCLUDE
        #include "../Utils/ReGizmo2DUtils.cginc"

        float4 frag (g2f_2d i) : SV_Target
        {
            float sdf = sdEquilateralTriangle(i.uv - float2(0.51, 0.3), 0.5);
            sdf = sample_sdf(sdf);

            //return lerp(float4(0,0,0,0.2), float4(i.color, sdf), sdf);

            clip(sdf == 0 ? -1 : 1);
            return float4(i.color, sdf);
        }
        ENDCG

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest LEqual
            ZWrite On
            Cull Off

            CGPROGRAM
            #pragma vertex vert_2d
            #pragma geometry geom_2d
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
            ENDCG
        }
    }
}
