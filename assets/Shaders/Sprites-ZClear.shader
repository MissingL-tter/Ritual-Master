Shader "Sprites/ZClear"
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
		ZWrite On
		Pass {
			Stencil {
				Comp Always
				Pass Zero
			}
		}
	}
}
