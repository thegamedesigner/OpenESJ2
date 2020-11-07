Shader "Effects/GradientShader" {
	Properties {
		[PerRendererData]
		_MainTex ("Sprite Texture", 2D) = "white" {}

		_Colour1 ("Colour1", Color) = (1,1,1,1)

		_Colour2 ("Colour2", Color) = (1,1,1,1)

		_SectionCount ("SeparationCount", Float) = 1

		[MaterialToggle]
		_Direction ("Flip Direction", Float) = 0

		[HideInInspector]
		_RendererColor ("RendererColor", Color) = (1,1,1,1)

		[HideInInspector]
		_Flip ("Flip", Vector) = (1,1,1,1)

		[PerRendererData]
		_AlphaTex ("External Alpha", 2D) = "white" {}

		[PerRendererData]
		_EnableExternalAlpha ("Enable External Alpha", Float) = 0
	}
	SubShader {
		Tags {
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata {
				half4 vertex : POSITION;
				half2 uv : TEXCOORD0;
			};

			struct v2f {
				half2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				half4 vertex : SV_POSITION;
				fixed separationSize : COLOR0;
			};

			sampler2D _MainTex;
			half4 _MainTex_ST;

			// The number of separations in the gradient.
			// EG: This value would be 1 if you wanted two sections.
			fixed _SectionCount;
			
			v2f vert (appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.separationSize = 1 / (_SectionCount + 1);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			// Direction is 
			fixed _Direction;

			// The initial colour the gradient "starts" from.
			fixed4 _Colour1;

			// The colour the gradient moves "to"
			fixed4 _Colour2;

			fixed4 frag (v2f i) : SV_Target {
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				half gradientPos = i.uv.y * step(_SectionCount, 0);
				gradientPos += (i.separationSize * ceil(i.uv.y / i.separationSize)) * step(1, _SectionCount);

				half gradientDirection = (gradientPos * _Direction) + ((gradientPos * -1 + 1) * ((_Direction * -1) + 1));

				fixed4 shaderColour = lerp(_Colour1, _Colour2, gradientDirection);

				UNITY_APPLY_FOG(i.fogCoord, col);
				return shaderColour;
			}
			ENDCG
		}
	}
}
