// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//Originally by mike_mac Enhancement: Sebastian Vargas - XOR Games ltda.

Shader "Custom/NoTexture_Additive" 
{
    Properties 
    {
        _Color ("Color Tint", Color) = (1,1,1,1)    
    }

    Category 
    {
        Lighting Off
        ZWrite Off
        AlphaTest Off
        Cull back
        Blend OneMinusDstColor One

        Tags {Queue=Transparent}

        SubShader {
            Pass {
                LOD 200
                CGPROGRAM

                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                float4 _Color;

                struct v2f {
                    float4  pos : SV_POSITION;
                };

                v2f vert (appdata_base v) {
                    v2f o;
                    o.pos = UnityObjectToClipPos (v.vertex);
                    return o;
                }

                half4 frag (v2f i) : COLOR {
                    half4 c = _Color;
                    c *= _Color[3] * c[3] ;
                    return c;
                }
                ENDCG

            }
        }
    }
    FallBack "Diffuse"
}

