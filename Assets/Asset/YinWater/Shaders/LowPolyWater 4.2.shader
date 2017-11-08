// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Yin/LowPolyWater 4.2" 
{
	Properties
	{
		_Color("Water Color", Color) = (1,1,1,1)
		_SpecColor("Specular Material Color", Color) = (1,1,1,1)
		_WaveAmplitude("Wave Amplitude", Range(0.1, 2.0)) = 0.5
		_WaveLength("Wave Length", Range(0.1, 10.0)) = 0.5		
		_WaveSpeed("Wave Speed", Range(0.1, 1.0)) = 0.2

		_Shininess("Shininess", Range(0.1, 100.0)) = 1.0
		_AlphaFactor("Alpha factor", Range(0, 1.0)) = 0.5
		_InvFadeParemeter("Auto blend parameter (Edge, Shore, Distance scale)", Vector) = (0.2 ,0.39, 0.5, 1.0)
		_DistortionStrength("Distortion Strength", Range(0.0, 0.01)) = 0.01		

		_Foam("Foam (intensity, cutoff)", Vector) = (0.1, 0.375, 0.0, 0.0)
		_ShoreColor("Shore Color", Color) = (1,1,1,1)
		_ShoreTex("Shore & Foam texture ", 2D) = "black" {}
		[HideInInspector] _ReflectionTex("Internal reflection", 2D) = "white" {}
		[HideInInspector] _RefractionTex("Internal refraction", 2D) = "white" {}
	}

	CGINCLUDE
		#define UNITY_SETUP_BRDF_INPUT SpecularSetup
	ENDCG

	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType" = "Opaque" } //"Geometry" "RenderType" = "Opaque" } 
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			Tags{ "LightMode" = "ForwardBase" }

			CGPROGRAM
				#pragma target 4.0
				#pragma exclude_renderers gles
				#pragma vertex vert
				#pragma geometry geom
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest

				#pragma multi_compile_fwdbase
				#pragma multi_compile_fog
				#include "UnityCG.cginc"
				#include "HLSLSupport.cginc"
				#include "UnityStandardCore.cginc"
				#include "noiseSimplex.cginc"
				
				// **************************************************************
				// Variables
				// **************************************************************

				uniform float _WaveAmplitude;
				uniform float _WaveLength;				
				uniform float _WaveSpeed;

				sampler2D _ShoreTex;
				sampler2D _ReflectionTex;
				sampler2D _RefractionTex;
				sampler2D _CameraDepthTexture;
				
				uniform float _Shininess;
				uniform float _AlphaFactor;
				uniform float _DistortionStrength;
				
				uniform float4 _Foam;
				uniform float4 _ShoreColor;
				uniform float4 _InvFadeParemeter;
				// **************************************************************
				// Data structures
				// **************************************************************

				struct appdata
				{
					float4 vertex : POSITION;
					float3 normal : NORMAL;
				};

				struct v2g
				{
					float4 pos                     : SV_POSITION;
					float4 refparam : TEXCOORD0;
					SHADOW_COORDS(1)
					UNITY_FOG_COORDS(2)
						float4 scrPos : TEXCOORD3;
					float4 viewInterpolator : TEXCOORD4;
					float4 bumpCoords : TEXCOORD5;
				};

				struct g2f
				{
					float4 pos                     : SV_POSITION;
					float4 scrPos : TEXCOORD0;
					float4 refparam : TEXCOORD1;
					SHADOW_COORDS(2)
					UNITY_FOG_COORDS(3)
					float3 diffuseColor : TEXCOORD4;
					float3 specularColor : TEXCOORD5;
					float4 viewInterpolator : TEXCOORD6;
					float4 bumpCoords : TEXCOORD7;
				};


				// **************************************************************
				// Shader Programs
				// **************************************************************

				// Vertex Shader ------------------------------------------------
				v2g vert(appdata v)
				{
					v2g o;
					UNITY_INITIALIZE_OUTPUT(v2g, o);

					half3 worldSpaceVertex = mul(unity_ObjectToWorld, (v.vertex)).xyz;

					float offset = _WaveAmplitude * snoise(_Time[1] * _WaveSpeed + v.vertex.xz / _WaveLength);
					v.vertex.y += offset;
					
					o.pos = v.vertex;

					o.scrPos = ComputeScreenPos(UnityObjectToClipPos(o.pos));
					COMPUTE_EYEDEPTH(o.scrPos.z);

					
					o.viewInterpolator.xyz = worldSpaceVertex - _WorldSpaceCameraPos;
					o.viewInterpolator.w = saturate(offset);

					float3 r = normalize(ObjSpaceViewDir(v.vertex));
					float d = saturate(dot(r, normalize(v.normal)));
					o.refparam = float4(d, 0, 0, 0);


					half2 tileableUv = mul(unity_ObjectToWorld, (v.vertex)).xz;
					o.bumpCoords.xyzw = tileableUv.xyxy;




					TRANSFER_SHADOW(o);
					UNITY_TRANSFER_FOG(o, o.pos);
					return o;
				}

				// Geometry Shader -----------------------------------------------------
				[maxvertexcount(3)]
				void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream)
				{
					g2f o;
					UNITY_INITIALIZE_OUTPUT(g2f, o);

					float3 v0 = IN[0].pos.xyz;
					float3 v1 = IN[1].pos.xyz;
					float3 v2 = IN[2].pos.xyz;

					float3 centerPos = (v0 + v1 + v2) / 3.0;

					float3 vn = normalize(cross(v1 - v0, v2 - v0));
					
					float3 normalDirection = normalize(mul(float4(vn, 0.0), unity_WorldToObject).xyz);
					float3 viewDirection = normalize(_WorldSpaceCameraPos - mul(unity_ObjectToWorld, float4(centerPos, 0.0)).xyz);
					float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);


					float attenuation = 1.0;

					float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;

					float3 diffuseReflection = attenuation * _LightColor0.rgb * _Color.rgb * max(0.0, dot(normalDirection, lightDirection));

					float3 specularReflection;
					if (dot(normalDirection, lightDirection) < 0.0)
					{
						specularReflection = float3(0.0, 0.0, 0.0);
					}
					else
					{
						specularReflection = attenuation * _LightColor0.rgb
							* _SpecColor.rgb * pow(max(0.0, dot(
								reflect(-lightDirection, normalDirection),
								viewDirection)), _Shininess);
					}
					
					o.pos = UnityObjectToClipPos(IN[0].pos);
					o.scrPos = ComputeScreenPos(o.pos);
					o.refparam = IN[0].refparam;
					TRANSFER_SHADOW(o);
					UNITY_TRANSFER_FOG(o, o.pos);
					o.diffuseColor = ambientLighting + diffuseReflection / 4;
					o.specularColor = specularReflection + diffuseReflection * 3 / 4;
					o.viewInterpolator = IN[0].viewInterpolator;
					o.bumpCoords = IN[0].bumpCoords;
					triStream.Append(o);
					
					o.pos = UnityObjectToClipPos(IN[1].pos);
					o.scrPos = ComputeScreenPos(o.pos);
					o.refparam = IN[1].refparam;
					TRANSFER_SHADOW(o);
					UNITY_TRANSFER_FOG(o, o.pos);
					o.diffuseColor = ambientLighting + diffuseReflection / 4;
					o.specularColor = specularReflection + diffuseReflection * 3 / 4;
					o.viewInterpolator = IN[1].viewInterpolator;
					o.bumpCoords = IN[1].bumpCoords;
					triStream.Append(o);
					
					o.pos = UnityObjectToClipPos(IN[2].pos);
					o.scrPos = ComputeScreenPos(o.pos);
					o.refparam = IN[2].refparam;
					TRANSFER_SHADOW(o);
					UNITY_TRANSFER_FOG(o, o.pos);
					o.diffuseColor = ambientLighting + diffuseReflection / 4;
					o.specularColor = specularReflection + diffuseReflection * 3 / 4;
					o.viewInterpolator = IN[2].viewInterpolator;
					o.bumpCoords = IN[2].bumpCoords;
					triStream.Append(o);
				}

				// Fragment Shader -----------------------------------------------
				half4 frag(g2f i) : COLOR
				{
					half4 edgeBlendFactors = half4(1.0, 0.0, 0.0, 0.0);

					float sceneZ = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture,UNITY_PROJ_COORD(i.scrPos));
					sceneZ = LinearEyeDepth(sceneZ);
					
					float partZ = i.scrPos.z;
					

					//edgeBlendFactors = saturate(_InvFadeParemeter * (sceneZ - partZ));
					//edgeBlendFactors.y = 1.0 - edgeBlendFactors.y;

					//half4 c = half4(1, 1, 1, 1);
					//half diff = abs(sceneZ - i.scrPos.z);// / 0.252;
					//diff = smoothstep(1.1, 1, diff);
					//c = lerp(lerp(c, _ShoreColor, _ShoreColor.a), c, diff);
					//return c;

					//float depth = LinearEyeDepth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos)).r);
					//depth -= i.scrPos.z;

					//float alpha = saturate(_AlphaFactor*depth);

					//i.diffuseColor = lerp(0, i.diffuseColor, fade);
					//i.specularColor = lerp(0, i.specularColor, fade);

					half2 ndc = (i.scrPos.xy / i.scrPos.w);
					half2 distortion = snoise(_Time[1] + i.scrPos.xy) * _DistortionStrength * _WaveSpeed;
					ndc += distortion;		

					half4 finalcolor = half4(0, 0, 0, 0);
					half4 color1;
					half4 color2;
										
					half shadowAtten = SHADOW_ATTENUATION(i);

					half4 refl = tex2D(_ReflectionTex, ndc);
					half4 refr = tex2D(_RefractionTex, ndc);

					half4 sho = tex2D(_ShoreTex, i.bumpCoords);
					//half4 sho = (tex2D(_ShoreTex, i.bumpCoords.xy *  2.0f) * tex2D(_ShoreTex, i.bumpCoords.zw *  2.0f)) - 0.125;


					color1 = lerp(refl, refr, i.refparam.r);
					color1.a = shadowAtten;
					color2 = half4(lerp(i.specularColor, i.diffuseColor, i.refparam.r), shadowAtten);



					//half4 color3 = (0, 0, 1, shoreAlpha);

					finalcolor = (color1 + color2) * 0.5;
					finalcolor.rgb += sho.rbg * _Foam.x *(edgeBlendFactors.y + saturate(i.viewInterpolator.w - _Foam.y));
					//finalcolor.rgb += sho.rbg *(edgeBlendFactors.y + saturate(i.viewInterpolator.w - _Foam.y));


					//finalcolor.rgb += 1.0 - edgeBlendFactors.x;
					//finalcolor.a = edgeBlendFactors.x;

					UNITY_APPLY_FOG(i.fogCoord, finalcolor);					
					return half4(finalcolor);
				}
			ENDCG
		}//End Pass
	}//End Subshader
	Fallback "Diffuse"
}//End Shader