Shader "Hidden/ReGizmo/CircleShader"
{
    Properties { }
    SubShader
    {
        Tags { "RenderType"="Overlay" "Queue"="Overlay" }

        CGINCLUDE
        #include "../Utils/ReGizmo2DUtils.cginc"

        // Compute the inverse of a 2-by-2 matrix  
        float2x2 inverse (float2x2 M)  {    
            return float2x2(M[1][1], -M[0][1], -M[1][0], M[0][0]) / determinant(M); 
        }  

        float4 frag (g2f_2d i) : SV_Target
        {
            /* static const int start_sample = -1;
            static const int samples = 3;

            float2 fw = fwidth(i.uv);
            float2 step = fw / (float)samples;

            float2 pos = start_sample * step;
            float2x2 J = transpose(float2x2(ddx(i.uv), ddy(i.uv)));
            float2x2 J_inv = inverse(J);

            float4 color = float4(i.color, 0.0);

            for (int x = 0; x < samples; x++, pos.x += step.x)
            {
                pos.y = start_sample * step.y;
                for (int y = 0; y < samples; y++, pos.y += step.y)
                {
                    float2 test = abs(mul(J_inv, pos));
                    if (test.x < 0.5 && test.y < 0.5)
                    {
                        float sdf = sdCircle((i.uv - 0.51) + pos, 0.5);
                        sdf *= sdCircle((i.uv - 0.51) + pos, 0.5 - i.inner_radius);
                        sdf = sample_sdf(sdf);
                        color.a += sdf;
                    }
                }
            }

            color.a *= rcp(24);
            color.a += pow(color.a, 0.3);
            color.a = pow(color.a, 2);
            clip(color.a == 0 ? -1 : 1);
            return color; */
            
            float2 fw = fwidth(i.uv);
            float2 pos = i.uv - 0.51;
            float sdf = sdCircle(pos, 0.5);
            sdf *= sdCircle(pos, 0.5 - (i.inner_radius + length(fw)));
            sdf = sample_sdf(sdf);
            sdf += pow(sdf, 2);

            /* float sdf = 0;
            for (int x = -3; x <= 3; x++)
            {
                for (int y = -3; y <= 3; y++)
                {
                    float2 p = pos + float2(x, y) * fw;
                    float _sdf = sdCircle(pos, 0.51);
                    _sdf *= sdCircle(pos, 0.51 - (i.inner_radius));
                    _sdf = sample_sdf(_sdf);

                    sdf += _sdf;
                }
            } 
            sdf *= rcp(36);
            */

            //return lerp(float4(1,1,1,0.2), float4(i.color, sdf), sdf);
            
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
