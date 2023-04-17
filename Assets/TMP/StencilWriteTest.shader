Shader "Custom/StencilWriteTest"
{
    SubShader
    {
        // The rest of the code that defines the SubShader goes here.
        Tags { "RenderType" = "Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert
        struct Input {
            float4 color : COLOR;
        };
        void surf(Input IN, inout SurfaceOutput o) {
            o.Albedo = 0;
        }
        ENDCG


    }
}