// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Yin/LowPolyWater"
{
	Properties
	{
		//_MainTex("Diffuse (RGB)", 2D) = "white" {}
		_Color("Water Color", Color) = (1,1,1,1)		
		_SpecColor("Specular Material Color", Color) = (1,1,1,1)
		_WaveLength("Wave Length", Range(0.1, 10.0)) = 0.5
		_WaveHeight("Wave Height", Range(0.1, 2.0)) = 0.5
		_WaveSpeed("Wave Speed", Range(0.1, 1.0)) = 0.2

		_Shininess("Shininess", Float) = 1.0

		_AlphaFactor("Alpha factor", Range(0, 1.0)) = 0.5

		_DistortionStrength("Distortion Strength", Range(0.01, 0.1)) = 0.01

		_ColorDepth("ColorDepth",Range(0,1)) = 0

		// reflection		
		[HideInInspector] _ReflectionTex("", 2D) = "white" {}		

		// refraction
		[HideInInspector] _RefractionTex("", 2D) = "white" {}
	}

	SubShader
	{		
		Pass
		{
			Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			#pragma target 3.0
			#pragma vertex vert
			#pragma geometry geom
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"
			#include "noiseSimplex.cginc"
#include "UnityStandardCore.cginc"
		
			// User-specified properties
			//uniform sampler2D _MainTex;
			float _WaveLength;
			float _WaveHeight;
			float _WaveSpeed;

			
			
			sampler2D _ReflectionTex;
			sampler2D _RefractionTex;
			sampler2D _CameraDepthTexture;



			float _ColorDepth;
			float _DistortionStrength;
			uniform float _Shininess;
			uniform float _AlphaFactor;

			uniform float4x4 _ProjMatrix;
			uniform float _Type;
			uniform float fresnel;

			

			struct v2g
			{
				float4 pos : SV_POSITION;
				float3 norm : NORMAL;
				float2 uv : TEXCOORD0;
				float4 scrPos : TEXCOORD1;
				float fresnel : TEXCOORD2;
				SHADOW_COORDS(4)
			};

			struct g2f
			{
				float4 pos : SV_POSITION;
				float3 norm : NORMAL;
				float2 uv : TEXCOORD0;
				float4 scrPos : TEXCOORD1;
				float fresnel : TEXCOORD2;
				fixed3 diffuseColor : TEXCOORD3;
				fixed3 specularColor : TEXCOORD4;
			};

			v2g vert(appdata_full v)
			{
				float3 v0 = mul((float3x3)unity_ObjectToWorld, v.vertex).xyz;

				v0.y += _WaveHeight * snoise(_Time[1] * _WaveSpeed + v0.xz / _WaveLength);

				v.vertex.xyz = mul((float3x3)unity_WorldToObject, v0);

				v2g o;
				o.pos = v.vertex;
				//o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.norm = v.normal;
				o.uv = v.texcoord;
				//o.uv = TRANSFORM_TEX(uv, _MainTex);
				o.scrPos = ComputeScreenPos(o.pos);				
				float3 r = normalize(ObjSpaceViewDir(v.vertex));
				o.fresnel = saturate(dot(r, normalize(v.normal)));		
				COMPUTE_EYEDEPTH(o.scrPos.z);
				TRANSFER_SHADOW(o);
				return o;
			}

			[maxvertexcount(3)]
			void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream)
			{
				float3 v0 = IN[0].pos.xyz;
				float3 v1 = IN[1].pos.xyz;
				float3 v2 = IN[2].pos.xyz;

				float3 centerPos = (v0 + v1 + v2) / 3.0;

				float3 vn = normalize(cross(v1 - v0, v2 - v0));

				float4x4 modelMatrix = unity_ObjectToWorld;
				float4x4 modelMatrixInverse = unity_WorldToObject;

				float3 normalDirection = normalize(mul(float4(vn, 0.0), modelMatrixInverse).xyz);
				float3 viewDirection = normalize(_WorldSpaceCameraPos - mul(modelMatrix, float4(centerPos, 0.0)).xyz);

				//commen out for testing, original code
				/*float attenuation = 1.0;
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);*/				

				float attenuation;
				float3 lightDirection;				

				if (_WorldSpaceLightPos0.w == 0.0) // directional light?
				{
					attenuation = 1.0; // no attenuation
					lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				}
				//else // point or spot light	 --------------------- not working
				//{
				//	float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - mul(modelMatrix, float4(centerPos, 0.0)).xyz;
				//	float distance = length(vertexToLightSource);
				//	
				//	lightDirection = normalize(vertexToLightSource);
				//	attenuation = 1.0 / 5 + 0.3 * distance;
				//}

				// Color of ambient light on a object = color of source light * color of the object
				float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;

				float NdotL = dot(normalDirection, lightDirection);

				float3 diffuseReflection = attenuation * _LightColor0.rgb * _Color.rgb * max(0.0, NdotL);

				float3 specularReflection;

				// if angle between normal and light direction > 90 degrees, no specular reflection
				if (NdotL < 0.0)
				{
					specularReflection = float3(0.0, 0.0, 0.0);
				}
				else
				{
					specularReflection = attenuation * _LightColor0.rgb * _SpecColor.rgb * pow(max(0.0, dot(
							reflect(-lightDirection, normalDirection), viewDirection)), _Shininess);// *length(ambientLighting);
				}

				g2f o;
				o.pos = UnityObjectToClipPos(IN[0].pos);
				o.norm = vn;
				o.uv = IN[0].uv;
				o.diffuseColor = ambientLighting + diffuseReflection / 4;
				o.specularColor = specularReflection + diffuseReflection * 3/ 4;
				o.scrPos = ComputeScreenPos(o.pos);
				//o.scrPos = IN[0].scrPos;
				o.fresnel = IN[0].fresnel;
				TRANSFER_SHADOW(o);
				triStream.Append(o);

				o.pos = UnityObjectToClipPos(IN[1].pos);
				o.norm = vn;
				o.uv = IN[1].uv;
				o.diffuseColor = ambientLighting + diffuseReflection / 4;
				o.specularColor = specularReflection + diffuseReflection * 3 / 4;
				o.scrPos = ComputeScreenPos(o.pos);
				//o.scrPos = IN[1].scrPos;
				o.fresnel = IN[1].fresnel;
				TRANSFER_SHADOW(o);
				triStream.Append(o);

				o.pos = UnityObjectToClipPos(IN[2].pos);
				o.norm = vn;
				o.uv = IN[2].uv;
				o.diffuseColor = ambientLighting + diffuseReflection / 4;
				o.specularColor = specularReflection + diffuseReflection * 3 / 4;
				o.scrPos = ComputeScreenPos(o.pos);
				//o.scrPos = IN[2].scrPos;
				o.fresnel = IN[2].fresnel;
				TRANSFER_SHADOW(o);
				triStream.Append(o);
			}

			half4 frag(g2f i) : COLOR
			{
				float depth = LinearEyeDepth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos)).r);
				depth -= i.scrPos.z;
			
				/*half4 edgeBlendFactors = half4(1.0, 0.0, 0.0, 0.0);
				edgeBlendFactors = saturate(_InvFadeParemeter * (depth - i.refl.w));
				edgeBlendFactors.y = 1.0 - edgeBlendFactors.y;
				float alpha = edgeBlendFactors.x;*/
				float alpha = saturate(_AlphaFactor*depth / 5);
				
				i.diffuseColor = lerp(0.4 * i.diffuseColor, i.diffuseColor, alpha);
				i.specularColor = lerp(0.4 * i.specularColor, i.specularColor, alpha);


				half2 ndc = (i.scrPos.xy / i.scrPos.w) ;
				half2 reflectTecCoords = half2 (ndc.x, ndc.y);
				half2 refractTecCoords = half2 (ndc.x, ndc.y);

				half2 distortion = snoise(_Time[1] + i.scrPos.xy) * _DistortionStrength;

				reflectTecCoords += distortion;
				refractTecCoords += distortion;

				half4 finalcolor = half4(0, 0, 0, 0);;
				half4 color1;
				half4 color2;

				if (_Type == 0)
				{
					half4 refl = tex2D(_ReflectionTex, reflectTecCoords);
					half4 refr = tex2D(_RefractionTex, refractTecCoords);
					//half4 refl = tex2Dproj(_ReflectionTex, UNITY_PROJ_COORD(i.scrPos));
					//half4 refr = tex2Dproj(_RefractionTex, UNITY_PROJ_COORD(i.scrPos));
					color1 = lerp(refl, refr, i.fresnel);
					color1.a = alpha;
					color2 = half4(lerp(i.specularColor, i.diffuseColor, i.fresnel), alpha);
					finalcolor = (color1 + color2) * 0.5;
				}
				else if (_Type == 1)
				{
					//half4 refl = tex2D(_ReflectionTex, (i.scrPos.xy) / i.scrPos.w);
					half4 refl = tex2Dproj(_ReflectionTex, UNITY_PROJ_COORD(i.scrPos));
					color1 = lerp(0, refl, i.fresnel);
					color2 = half4(lerp(i.specularColor, i.diffuseColor, i.fresnel), alpha);
					finalcolor = (color1 + color2) * 1;
				}
				else if (_Type == 2)
				{					
					half4 refr = tex2Dproj(_RefractionTex, UNITY_PROJ_COORD(i.scrPos));
					color1 = lerp(refr, 0, i.fresnel);
					color2 = half4(lerp(i.specularColor, i.diffuseColor, i.fresnel), alpha);
					finalcolor = (color1 + color2) * 1;
				}

				return half4(finalcolor);
			}
			ENDCG
		}

		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }

			Fog{ Mode Off }
			ZWrite On ZTest Less Cull Off
			Offset[_ShadowBias],[_ShadowBiasSlope]

			CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_shadowcaster
#pragma fragmentoption ARB_precision_hint_fastest

#include "UnityCG.cginc"
#include "noiseSimplex.cginc"

		float _WaveLength;
		float _WaveHeight;
		float _WaveSpeed;

		struct v2f
		{
			V2F_SHADOW_CASTER;
		};

		v2f vert(appdata_full v)
		{
			float3 v0 = mul((float3x3)unity_ObjectToWorld, v.vertex).xyz;

			v0.y += _WaveHeight * snoise(_Time[1] * _WaveSpeed + v0.xz / _WaveLength);

			v.vertex.xyz = mul((float3x3)unity_WorldToObject, v0);

			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			return o;
		}

		float4 frag(v2f i) : COLOR
		{
			SHADOW_CASTER_FRAGMENT(i)
		}
			ENDCG
		}
	}
	Fallback "Diffuse"
}