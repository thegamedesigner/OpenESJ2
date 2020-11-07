// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/NoTexture_Multiply" {
	Properties {
		_Color ("Color", Color) = (0,0,0,0)
	}

	Category {
		Lighting Off
        ZWrite Off
        AlphaTest Off
        Cull back
        Blend DstColor Zero

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
		            float4  color : COLOR;
		        };

		        v2f vert (appdata_full v) {
		            v2f o;
		            o.color = _Color;
		            o.pos = UnityObjectToClipPos (v.vertex);
		            return o;
		        }

		        half4 frag (v2f i) : COLOR {
		            half4 c = i.color;
		            return c;
		        }
		        ENDCG
		    }
		} 
	}
	FallBack "Diffuse"
}
