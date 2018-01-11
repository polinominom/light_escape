// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "AreaRange/NarrowRangeShader"
{
	Properties
	{
		_Color("Color",Color) = (1,0,0,0)
		_Radius("Radius", Float) = 0
		_Limit("Limit", FLoat) = 0
		_MainTex ("Texture", 2D) = "white" {}
		_Angle("Angle", Float) = 30
		_PI("Pi", Range(3.1415926535897932384626433,3.1415926535897932384626433)) = 3.1415926535897932384626433
		_Alpha("Alpha", Range(0,1)) = 1
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
			float _Angle;
			float _PI;
			float _Alpha;

			fixed4 frag (v2f i) : SV_Target
			{
				float x = i.uv.x - 0.5;
				float y = i.uv.y - 0.5;
				float d = sqrt(pow(x, 2)+pow(y, 2));
				fixed4 col = tex2D(_MainTex, i.uv);

				// just add the colors
				col *= _Color;

				// if radius >= d: col.a = 1 else: col.a = 0
				col.a = step(d, _Radius) * _Alpha;

				// if col.a >= 1 && _Angle >= limit
				float limitangle = 180 - _Angle;
				float theangle = atan2(y, x)* 180/_PI;
				col.a = step(_Alpha, col.a)*step(limitangle, theangle)*col.a;

				return col;

			}
			ENDCG
		}
	}
}
