// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Yin/LowPolyWater 4.0" 
{
	Properties
	{
		_MainTex("Diffuse (RGB)", 2D) = "white" {}
		_Color("Water Color", Color) = (1,1,1,1)
		_SpecColor("Specular Material Color", Color) = (1,1,1,1)
		_WaveAmplitude("Wave Amplitude", Range(0.1, 2.0)) = 0.5
		_WaveLength("Wave Length", Range(0.1, 10.0)) = 0.5		
		_WaveSpeed("Wave Speed", Range(0.1, 1.0)) = 0.2

		_Shininess("Shininess", Range(0.1, 100.0)) = 1.0
		_AlphaFactor("Alpha factor", Range(0, 1.0)) = 0.5
		_DistortionStrength("Distortion Strength", Range(0.0, 0.01)) = 0.01		

		[HideInInspector] _ReflectionTex("", 2D) = "white" {}
		[HideInInspector] _RefractionTex("", 2D) = "white" {}
	}

	CGINCLUDE
		#define UNITY_SETUP_BRDF_INPUT SpecularSetup
	ENDCG

	SubShader
	{
		Tags { "Queue" = "Geometry" "RenderType" = "Opaque" } //"Geometry" "RenderType" = "Opaque" } 
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

				sampler2D _ReflectionTex;
				sampler2D _RefractionTex;
				sampler2D _CameraDepthTexture;
				
				uniform float _Shininess;
				uniform float _AlphaFactor;
				uniform float _DistortionStrength;
				
				// **************************************************************
				// Data structures
				// **************************************************************

				struct vFwdBase
				{
					float4 pos                     : SV_POSITION;
					float4 tex                     : TEXCOORD0;
					float4 scrPos : TEXCOORD1;
					float fresnel : TEXCOORD2;
					SHADOW_COORDS(6)
					UNITY_FOG_COORDS(7)
					float4 posWorld                  : TEXCOORD8;
					float3 diffuseColor : TEXCOORD9;
					float3 specularColor : TEXCOORD10;
				};

				// **************************************************************
				// Shader Programs
				// **************************************************************

				// Vertex Shader ------------------------------------------------
				vFwdBase vert(VertexInput v)
				{
					vFwdBase o;
					UNITY_INITIALIZE_OUTPUT(vFwdBase, o);

					v.vertex.y += _WaveAmplitude * snoise(_Time[1] * _WaveSpeed + v.vertex.xz / _WaveLength);
					
					float4 posWorld = mul(unity_ObjectToWorld, v.vertex);
										
					o.pos = v.vertex;
					o.tex = TexCoords(v);				
					o.scrPos = ComputeScreenPos(o.pos);
					COMPUTE_EYEDEPTH(o.scrPos.z);

					float3 r = normalize(ObjSpaceViewDir(v.vertex));
					o.fresnel = saturate(dot(r, normalize(v.normal)));

					TRANSFER_SHADOW(o);
					UNITY_TRANSFER_FOG(o, o.pos);

					o.diffuseColor = float3(0.0, 0.0, 0.0);
					o.specularColor = float3(0.0, 0.0, 0.0);

					return o;
				}

				// Geometry Shader -----------------------------------------------------
				[maxvertexcount(3)]
				void geom(triangle vFwdBase IN[3], inout TriangleStream<vFwdBase> triStream)
				{
					vFwdBase o;
					UNITY_INITIALIZE_OUTPUT(vFwdBase, o);

					float3 v0 = IN[0].pos.xyz;
					float3 v1 = IN[1].pos.xyz;
					float3 v2 = IN[2].pos.xyz;

					float3 centerPos = (v0 + v1 + v2) / 3.0;

					float3 vn = normalize(cross(v1 - v0, v2 - v0));

					float4x4 modelMatrix = unity_ObjectToWorld;
					float4x4 modelMatrixInverse = unity_WorldToObject;

					float3 normalDirection = normalize(
						mul(float4(vn, 0.0), modelMatrixInverse).xyz);
					float3 viewDirection = normalize(_WorldSpaceCameraPos
						- mul(modelMatrix, float4(centerPos, 0.0)).xyz);
					float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
					float attenuation = 1.0;

					float3 ambientLighting =
						UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;

					float3 diffuseReflection =
						attenuation * _LightColor0.rgb * _Color.rgb
						* max(0.0, dot(normalDirection, lightDirection));

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
					o.tex = IN[0].tex;					
					o.scrPos = ComputeScreenPos(o.pos);
					o.fresnel = IN[0].fresnel;
					TRANSFER_SHADOW(o);
					UNITY_TRANSFER_FOG(o, o.pos);
					o.diffuseColor = ambientLighting + diffuseReflection / 4;
					o.specularColor = specularReflection + diffuseReflection * 3 / 4;
					triStream.Append(o);
					
					o.pos = UnityObjectToClipPos(IN[1].pos);
					o.tex = IN[1].tex;
					o.scrPos = ComputeScreenPos(o.pos);
					o.fresnel = IN[1].fresnel;
					TRANSFER_SHADOW(o);
					UNITY_TRANSFER_FOG(o, o.pos);
					o.diffuseColor = ambientLighting + diffuseReflection / 4;
					o.specularColor = specularReflection + diffuseReflection * 3 / 4;
					triStream.Append(o);
					
					o.pos = UnityObjectToClipPos(IN[2].pos);
					o.tex = IN[2].tex;
					o.scrPos = ComputeScreenPos(o.pos);
					o.fresnel = IN[2].fresnel;
					TRANSFER_SHADOW(o);
					UNITY_TRANSFER_FOG(o, o.pos);
					o.diffuseColor = ambientLighting + diffuseReflection / 4;
					o.specularColor = specularReflection + diffuseReflection * 3 / 4;
					triStream.Append(o);
				}

				// Fragment Shader -----------------------------------------------
				half4 frag(vFwdBase i) : COLOR
				{
					float depth = LinearEyeDepth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos)).r);
					depth -= i.scrPos.z;

					float alpha = saturate(_AlphaFactor*depth / 5);

					i.diffuseColor = lerp(0.4 * i.diffuseColor, i.diffuseColor, alpha);
					i.specularColor = lerp(0.4 * i.specularColor, i.specularColor, alpha);

					half2 ndc = (i.scrPos.xy / i.scrPos.w);
					half2 reflectTecCoords = ndc;
					half2 refractTecCoords = ndc;

					half2 distortion = snoise(_Time[1] + i.scrPos.xy) * _DistortionStrength;

					reflectTecCoords += distortion;
					refractTecCoords += distortion;					

					half4 finalcolor = half4(0, 0, 0, 0);;
					half4 color1;
					half4 color2;
										
					half shadowAtten = 0.3 + SHADOW_ATTENUATION(i);

					half4 refl = tex2D(_ReflectionTex, reflectTecCoords);
					half4 refr = tex2D(_RefractionTex, refractTecCoords);					
					color1 = lerp(refl, refr, i.fresnel);
					color1.a = shadowAtten;
					color2 = half4(lerp(i.specularColor, i.diffuseColor, i.fresnel), shadowAtten);
					finalcolor = (color1 + color2) * 0.5;

					UNITY_APPLY_FOG(i.fogCoord, color2);

					return half4(finalcolor);
				}
			ENDCG
		}//End Pass
	}//End Subshader
	Fallback "Diffuse"
}//End Shader