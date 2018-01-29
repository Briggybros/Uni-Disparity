﻿Shader "Custom/ColourChangeSurfaceShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { 
			"QueueType"="Transparent"
			"RenderType"="Transparent" 
			"IgnoreProjector"="True"
			"CanUseSpriteAtlas"="True"  }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _AlphaTex;
		float _AlphaSplitEnabled;
		sampler2D _SwapTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		fixed4 SampleSpriteTexture(float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);
				if(_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
				return color;
			}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			
			fixed4 c = SampleSpriteTexture (IN.uv_MainTex);
			fixed4 swapCol = tex2D(_SwapTex, float2(c.r, 0));
			fixed4 final = lerp(c, swapCol, swapCol.a);
			final.a = c.a;
			final.rgb *= c.a;
			o.Albedo = final;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
