Shader "Custom/BeAttackTest" {
Properties{
_MainTex("texture", 2D) = "black"{}
_Color("add color", Color) = (1,1,1,1)
//_BeAttack("BeAttack",Int)=0
}

SubShader{
Tags{ "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }
ZWrite Off
Blend SrcAlpha OneMinusSrcAlpha
LOD 100
Cull Off//设置双面渲染，避免角色缩放翻转时无渲染情况

Pass{
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

sampler2D _MainTex;
fixed4 _MainTex_ST;
fixed4 _Color;
int _BeAttack;//对外参数表示是否被攻击了

struct vIn {
half4 vertex:POSITION;
float2 texcoord:TEXCOORD0;
fixed4 color : COLOR;
};

struct vOut {
half4 pos:SV_POSITION;
float2 uv:TEXCOORD0;
fixed4 color : COLOR;
};

vOut vert(vIn v) {
vOut o;
o.pos = UnityObjectToClipPos(v.vertex);
o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
o.color = v.color;
return o;
}

fixed4 frag(vOut i) :COLOR{
fixed4 tex = tex2D(_MainTex, i.uv).rgba;
/*
if (tex.a == 1)return fixed4(1, 1, 1, 1);
else return fixed4(0, 0, 0, 0);
*/
if (_BeAttack==1) {//是否被攻击
if (tex.a == 1)return fixed4(1, 1, 1, 1);//对透明度为1的像素输出为白色
else return fixed4(0, 0, 0, 0);
}
else {
return tex;
}

}
ENDCG
}
}
}