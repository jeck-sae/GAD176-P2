Shader "Custom/FrameDifference" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _PreviousFrame ("Previous Frame", 2D) = "white" {}
    }
    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform sampler2D _MainTex;
            uniform float _bwBlend;
            uniform sampler2D _PreviousFrame;

            float4 frag(v2f_img i) : COLOR {
                float4 c = tex2D(_MainTex, i.uv);
                float4 p = tex2D(_PreviousFrame, i.uv);
                
                float4 result = float4(
                    c.r - p.r, 
                    c.g - p.g, 
                    c.b - p.b, 
                    1);
                    
                result.r += 0.5;
                result.g += 0.5;
                result.b += 0.5;

                return result;
            }
            ENDCG
        }
    }
}