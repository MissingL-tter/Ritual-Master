Shader "Sprites/Mask"
{
	Properties
	{
		[PerRendererData] _MainTex ("Mask Texture", 2D) = "white" {}
		_Cutoff ("Base Alpha Cutoff", Range(0,0.9)) = 0.5
	}

	SubShader
	{
		Tags { 
			"Queue"="Transparent+1"
		}
		ColorMask 0
		ZWrite Off
		Pass {
			Stencil {
				Ref 255
				Comp Always
				Pass Replace
			}
		}
	}
}
