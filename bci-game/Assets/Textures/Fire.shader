Shader "Custom/Fire"
{
   Properties {
        _MainTex ("Texture", 2D) = "white" { }
        _Speed ("Speed", Range(0, 10)) = 1
    }

    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Standard

        sampler2D _MainTex;
        fixed _Speed;

        struct Input {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutputStandard o) {
            fixed2 uv = IN.uv_MainTex;
            uv.y += sin(_Time.y * _Speed + uv.x * 10) * 0.1; // Adjust the multiplier for intensity

            o.Albedo = tex2D(_MainTex, uv).rgb;
        }
        ENDCG
    }

    FallBack "Diffuse"
}