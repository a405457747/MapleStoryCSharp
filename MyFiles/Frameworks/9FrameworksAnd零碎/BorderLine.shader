// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/BorderLine"
{
 Properties
 {
 _Edge ("Edge", Range(0, 0.2)) = 0.043
 _EdgeColor ("EdgeColor", Color) = (1, 1, 1, 1)
 _MainTex ("MainTex", 2D) = "white" {}
 }
 SubShader
 {
 Pass
 {
 CGPROGRAM
 #pragma vertex vert
 #pragma fragment frag
 #include "UnityCG.cginc"
 
 fixed _Edge;
 fixed4 _EdgeColor;
 sampler2D _MainTex;
 
 struct appdata
 {
 float4 vertex : POSITION;
 fixed2 uv : TEXCOORD0;
 };
 
 struct v2f
 {
 float4 vertex : SV_POSITION;
 float4 objVertex : TEXCOORD0;
 fixed2 uv : TEXCOORD1;
 };
 
 v2f vert (appdata v)
 {
 v2f o;
 o.vertex = UnityObjectToClipPos(v.vertex);
 o.objVertex = v.vertex;
 o.uv = v.uv;
 
 return o;
 }
 
 fixed4 frag (v2f i) : SV_Target
 { 
 fixed x = i.uv.x;
 fixed y = i.uv.y;
  
 if((x < _Edge) || (abs(1 - x) < _Edge) || (y < _Edge) || (abs(1 - y) < _Edge)) 
 {
  return _EdgeColor * abs(cos(_Time.y));
 }
 else 
 {
  fixed4 color = tex2D(_MainTex, i.uv);
  return color;
 }
 
 //return i.objVertex;
 //return fixed4(i.uv, 0, 1);
 }
 ENDCG
 }
 }
}
