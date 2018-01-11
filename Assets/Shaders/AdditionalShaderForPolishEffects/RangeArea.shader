// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/RangeArea"
{
	Properties
	{
		_Color("Color",Color) = (1,0,0,0)
		_Radius("Radius", Float) = 0
		_Limit("Limit", FLoat) = 0
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		ZWrite off
        Blend SrcAlpha OneMinusSrcAlpha // Alpha blending

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			fixed4 _Color;
			float _Radius;
			float _Limit;

			fixed4 frag (v2f i) : SV_Target
			{
				float d = sqrt(pow(i.uv.x - 0.5, 2)+pow(i.uv.y - 0.5, 2));
				fixed4 col = tex2D(_MainTex, i.uv);

				// just add the colors
				col *= _Color;

				// if radius >= d: col.a = 0.5 else: col.a = 0
				col.a = step(d, _Radius) * 0.5;

				// if col.a >= 0.5 && d >= limit
				col.a = step(0.5,col.a)*step(_Limit, d)*col.a;

				return col;

			}
			ENDCG
		}
	}
}
