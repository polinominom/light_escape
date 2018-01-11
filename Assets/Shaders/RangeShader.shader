// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

 Shader "Custom/Circle" {
      Properties {
          _Color ("Color", Color) = (1,0,0,0)
          _Thickness("Thickness", Range(0.0,0.5)) = 0.05
          _Radius("Radius", Range(0.0, 0.5)) = 0.4
          _Dropoff("Dropoff", Range(0.01, 4)) = 0.1
          [PerRendererData] _MainTex("Main Texture", 2D) = "white" {}
      }
      SubShader {
		

          Pass {
          	  ZWrite off
              Blend SrcAlpha OneMinusSrcAlpha // Alpha blending
              LOD 100
              Tags
              	{		
              		"Queue"="Transparent"

				}
              CGPROGRAM
             
              #pragma vertex vert
              #pragma fragment frag
              #include "UnityCG.cginc"
            
             	
              fixed4 _Color; // low precision type is usually enough for colors
              float _Thickness;
              float _Radius;
              float _Dropoff;
             
              struct fragmentInput {
                  float4 pos : SV_POSITION;
                  float2 uv : TEXTCOORD0;
              };
  
              fragmentInput vert (appdata_base v)
              {
                  fragmentInput o;
  
                  o.pos = UnityObjectToClipPos (v.vertex);
                  o.uv = v.texcoord.xy - fixed2(0.5,0.5);
  
                  return o;
              }
  
              // r = radius
              // d = distance
              // t = thickness
              // p = % thickness used for dropoff
              float antialias(float r, float d, float t, float p) {
                  if( d < (r - 0.5*t))
                     return - pow( d - r + 0.5*t,2)/ pow(p*t, 2) + 1.0;
                 else if ( d > (r + 0.5*t))
                     return - pow( d - r - 0.5*t,2)/ pow(p*t, 2) + 1.0; 
                 else
                     return 1.0;
              }
              
              fixed4 frag(fragmentInput i) : SV_Target {
                 float distance = sqrt(pow(i.uv.x, 2) + pow(i.uv.y,2));

				 float _r = _Radius ;//+ sin(_Time[1]);
                 return fixed4(_Color.r, _Color.g, _Color.b, _Color.a*antialias(_r, distance, _Thickness, _Dropoff));
              }
              
              
              ENDCG
          }
      }
  }