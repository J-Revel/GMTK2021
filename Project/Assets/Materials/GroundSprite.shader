 Shader "Sprites/Shadow Sprite"
 {
    Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_BumpMap("Normalmap", 2D) = "bump" {}
		_BumpIntensity("NormalMap Intensity", Range(-1, 2)) = 1
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_Cutoff("Alpha Cutoff", Range(0,1)) = 0.5
 
	}
 
		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
 
	}
    LOD 300
 
    // Now render front faces first
    Cull Back
    Lighting On
    ZWrite On

    CGPROGRAM
#pragma surface surf Lambert alpha vertex:vert addshadow
#pragma multi_compile DUMMY PIXELSNAP_ON 
 
    sampler2D _MainTex;
	sampler2D _BumpMap;
	fixed _BumpIntensity;
	fixed4 _Color;
	float _ScaleX;
 
	struct Input
	{
		float2 uv_MainTex;
		float2 uv_BumpMap;
		fixed4 color : COLOR;
	};
 
	void vert(inout appdata_full v, out Input o)
	{
#if defined(PIXELSNAP_ON) && !defined(SHADER_API_FLASH)
		v.vertex = UnityPixelSnap(v.vertex);
#endif
		float3 normal = v.normal;
 
		v.normal = float3(0,0,1);
		v.tangent = float4(1, 0, 0, 1);
 
		UNITY_INITIALIZE_OUTPUT(Input, o);
		o.color += _Color;
	}
 
	void surf(Input IN, inout SurfaceOutput o)
	{
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		_BumpIntensity = 1 / _BumpIntensity;
		o.Normal.z = -o.Normal.z * _BumpIntensity;
		o.Normal = normalize((half3)o.Normal);
	}
	ENDCG
	}
 
	Fallback Off
 }