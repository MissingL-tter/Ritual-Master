Shader "Sprites/InverseMask"
{
	Properties
	{
		[PerRendererData] _MainTex ("Mask Texture", 2D) = "white" {}
		_Cutoff ("Base Alpha Cutoff", Range(0,0.9)) = 0.5
	}

	SubShader
	{
		Tags { 
			"Queue"="Transparent+2"
		}
		ColorMask 0
		ZWrite On
		Pass {
			AlphaTest Less [_Cutoff]
			SetTexture [_MainTex] {
				combine texture * primary, texture
			}
		}
	}
}
