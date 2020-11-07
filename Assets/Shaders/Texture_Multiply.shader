// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Texture_Multiply" {
	Properties {
		_Color ("Color", Color) = (0,0,0,0)
		_MainTex ("Base (RGB)", 2D) = "white" {}

	}

	Category {
		Lighting Off
        ZWrite Off
        AlphaTest Off
        Cull back
        Blend DstColor Zero

        Tags {"Queue"="Transparent"}

		SubShader {
			Pass {
				LOD 200
				
				CGPROGRAM

		        #pragma vertex vert
		        #pragma fragment frag
		        #include "UnityCG.cginc"

		        float4 _Color;
		        sampler2D _MainTex;

		        float4 _MainTex_ST;

		        struct v2f {
		            float4  pos : SV_POSITION;
		            float2  uv : TEXCOORD0;
		            float4  color : COLOR;
		        };

		        v2f vert (appdata_full v) {
		            v2f o;
		            o.color = _Color;
		            o.pos = UnityObjectToClipPos (v.vertex);
		            o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
		            return o;
		        }

		        half4 frag (v2f i) : COLOR {
		        	half4 c = tex2D (_MainTex, i.uv) * i.color;
		            return c;
		        }
		        ENDCG
		    }
		} 
	}
	FallBack "Diffuse"
}
