// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//Shader "Example/Diffuse Distance" {
//	Properties {
//		_MainTex ("Texture", 2D) = "white" {}
//		_Center ("Center", Vector) = (0,0,0,0)
//		_Radius ("Radius", Float) = 0.5
//	}
//	SubShader {
//		Tags { "RenderType" = "Opaque" }
//		CGPROGRAM
//		#pragma surface surf Lambert
//		struct Input {
//			float2 uv_MainTex;
//			float3 worldPos;
//		};
//		sampler2D _MainTex;
//		float3 _Center;
//		float _Radius;
//
//		void surf (Input IN, inout SurfaceOutput o) {
//
//			o.Albedo = step(tex2D (_MainTex, IN.uv_MainTex).g, 0.5);
////			float d = distance(_Center, IN.worldPos);
////			float dN = 1 - saturate(d / _Radius);
////			
////			if (dN > 0.25 && dN < 0.3)
////				o.Albedo = half3(1,1,1);
////			else
////				o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
//		}
//
//		ENDCG
//	} 
//	Fallback "Diffuse"
//}

Shader "Custom/ShineEnemy"
 {
     Properties
     {
         [PerRendererData] _MainTex ("Main Texture", 2D) = "white" {}
         _Color ("Tint", Color) = (1,1,1,1)
         [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
         _EffectAmount ("Effect Amount", Range (0, 1)) = 1.0
     }
  
     SubShader
     {
         Tags
         {
             "Queue"="Overlay"
             "IgnoreProjector"="True"
             "RenderType"="Overlay"
             "PreviewType"="Plane"
             "CanUseSpriteAtlas"="True"
         }
  
         Cull Off
         Lighting Off
         ZWrite Off
         Fog { Mode Off }
         Blend SrcAlpha OneMinusSrcAlpha
  
         Pass
         {
         CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile DUMMY PIXELSNAP_ON
             #include "UnityCG.cginc"
            
             struct appdata_t
             {
                 float4 vertex   : POSITION;
                 float4 color    : COLOR;
                 float2 texcoord : TEXCOORD0;
             };
  
             struct v2f
             {
                 float4 vertex   : SV_POSITION;
                 fixed4 color    : COLOR;
                 half2 texcoord  : TEXCOORD0;
             };
            
             fixed4 _Color;
  
             v2f vert(appdata_t IN)
             {
                 v2f OUT;
                 OUT.vertex = UnityObjectToClipPos(IN.vertex);
                 OUT.texcoord = IN.texcoord;
                 OUT.color = IN.color * _Color;
                 #ifdef PIXELSNAP_ON
                 OUT.vertex = UnityPixelSnap (OUT.vertex);
                 #endif
  
                 return OUT;
             }
  
             sampler2D _MainTex;
             uniform float _EffectAmount;
  
             fixed4 frag(v2f IN) : COLOR
             {
                 half4 texcol = tex2D (_MainTex, IN.texcoord);              
                 texcol.rgb = lerp(texcol.rgb, _Color.rgb, _EffectAmount);
                 return texcol;
             }
         ENDCG
         }
     }
     Fallback "Sprites/Default"
 }