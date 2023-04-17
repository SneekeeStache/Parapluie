// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Crayon"
{
	Properties
	{
		_TopTexture3("Top Texture 3", 2D) = "white" {}
		_TopTexture1("Top Texture 1", 2D) = "white" {}
		_TopTexture2("Top Texture 2", 2D) = "white" {}
		_TopTexture0("Top Texture 0", 2D) = "white" {}
		_BaseCellOffset("Base Cell Offset", Range( -1 , 1)) = 0.01176471
		_HighlightOffset("Highlight Offset", Range( -1 , 1)) = -0.492891
		_ShadowTextureTile("Shadow Texture Tile", Float) = 2
		_ShadowTextureFalloff("Shadow Texture Falloff", Float) = 18.08
		_BaseCellSharpness("Base Cell Sharpness", Range( 0.01 , 1)) = 0.1497647
		_HighlightlSharpness("Highlight Sharpness", Range( 0.01 , 1)) = 0.01
		_BaseColor("Base Color", Color) = (1,0.5613208,0.5613208,0)
		_ShadowIntensity("Shadow Intensity", Range( 0 , 1)) = 0.5294118
		_HighlightColor("Highlight Color", Color) = (0.6525454,0.9659855,0.9811321,0)
		_DetailMapTile("Detail Map Tile", Float) = 2.5
		_DetailMaskTile("Detail Mask Tile", Float) = 2
		_DetailMapFalloff("Detail Map Falloff", Float) = 20
		_DetailMapIntensity("Detail Map Intensity", Range( 0 , 1)) = 0.2239311
		_InkSpotsTile("Ink Spots Tile", Float) = 1
		_FScale("F Scale", Float) = 25.13
		_FPower("F Power", Float) = 10.15
		_FContrast("F Contrast", Float) = 0.68
		_RimLightColor("Rim Light Color", Color) = (0.6560061,1,0.6556604,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		Stencil
		{
			Ref 2
			Comp Always
			Pass Replace
		}
		CGINCLUDE
		#include "UnityCG.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
		};

		sampler2D _TopTexture1;
		uniform float _DetailMapTile;
		uniform float _DetailMapFalloff;
		uniform float _DetailMapIntensity;
		sampler2D _TopTexture2;
		uniform float _DetailMaskTile;
		uniform float _HighlightOffset;
		uniform float _HighlightlSharpness;
		uniform float4 _BaseColor;
		sampler2D _TopTexture0;
		uniform float _ShadowTextureTile;
		uniform float _ShadowTextureFalloff;
		uniform float _FScale;
		uniform float _FPower;
		uniform float _FContrast;
		uniform float _BaseCellOffset;
		uniform float _BaseCellSharpness;
		uniform float _ShadowIntensity;
		uniform float4 _RimLightColor;
		uniform float4 _HighlightColor;
		sampler2D _TopTexture3;
		uniform float _InkSpotsTile;


		inline float4 TriplanarSampling39( sampler2D topTexMap, float3 worldPos, float3 worldNormal, float falloff, float2 tiling, float3 normalScale, float3 index )
		{
			float3 projNormal = ( pow( abs( worldNormal ), falloff ) );
			projNormal /= ( projNormal.x + projNormal.y + projNormal.z ) + 0.00001;
			float3 nsign = sign( worldNormal );
			half4 xNorm; half4 yNorm; half4 zNorm;
			xNorm = tex2D( topTexMap, tiling * worldPos.zy * float2(  nsign.x, 1.0 ) );
			yNorm = tex2D( topTexMap, tiling * worldPos.xz * float2(  nsign.y, 1.0 ) );
			zNorm = tex2D( topTexMap, tiling * worldPos.xy * float2( -nsign.z, 1.0 ) );
			return xNorm * projNormal.x + yNorm * projNormal.y + zNorm * projNormal.z;
		}


		inline float4 TriplanarSampling40( sampler2D topTexMap, float3 worldPos, float3 worldNormal, float falloff, float2 tiling, float3 normalScale, float3 index )
		{
			float3 projNormal = ( pow( abs( worldNormal ), falloff ) );
			projNormal /= ( projNormal.x + projNormal.y + projNormal.z ) + 0.00001;
			float3 nsign = sign( worldNormal );
			half4 xNorm; half4 yNorm; half4 zNorm;
			xNorm = tex2D( topTexMap, tiling * worldPos.zy * float2(  nsign.x, 1.0 ) );
			yNorm = tex2D( topTexMap, tiling * worldPos.xz * float2(  nsign.y, 1.0 ) );
			zNorm = tex2D( topTexMap, tiling * worldPos.xy * float2( -nsign.z, 1.0 ) );
			return xNorm * projNormal.x + yNorm * projNormal.y + zNorm * projNormal.z;
		}


		inline float4 TriplanarSampling21( sampler2D topTexMap, float3 worldPos, float3 worldNormal, float falloff, float2 tiling, float3 normalScale, float3 index )
		{
			float3 projNormal = ( pow( abs( worldNormal ), falloff ) );
			projNormal /= ( projNormal.x + projNormal.y + projNormal.z ) + 0.00001;
			float3 nsign = sign( worldNormal );
			half4 xNorm; half4 yNorm; half4 zNorm;
			xNorm = tex2D( topTexMap, tiling * worldPos.zy * float2(  nsign.x, 1.0 ) );
			yNorm = tex2D( topTexMap, tiling * worldPos.xz * float2(  nsign.y, 1.0 ) );
			zNorm = tex2D( topTexMap, tiling * worldPos.xy * float2( -nsign.z, 1.0 ) );
			return xNorm * projNormal.x + yNorm * projNormal.y + zNorm * projNormal.z;
		}


		inline float4 TriplanarSampling54( sampler2D topTexMap, float3 worldPos, float3 worldNormal, float falloff, float2 tiling, float3 normalScale, float3 index )
		{
			float3 projNormal = ( pow( abs( worldNormal ), falloff ) );
			projNormal /= ( projNormal.x + projNormal.y + projNormal.z ) + 0.00001;
			float3 nsign = sign( worldNormal );
			half4 xNorm; half4 yNorm; half4 zNorm;
			xNorm = tex2D( topTexMap, tiling * worldPos.zy * float2(  nsign.x, 1.0 ) );
			yNorm = tex2D( topTexMap, tiling * worldPos.xz * float2(  nsign.y, 1.0 ) );
			zNorm = tex2D( topTexMap, tiling * worldPos.xy * float2( -nsign.z, 1.0 ) );
			return xNorm * projNormal.x + yNorm * projNormal.y + zNorm * projNormal.z;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Normal = float3(0,0,1);
			float2 appendResult45 = (float2(_DetailMapTile , _DetailMapTile));
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float4 triplanar39 = TriplanarSampling39( _TopTexture1, ase_worldPos, ase_worldNormal, _DetailMapFalloff, appendResult45, 1.0, 0 );
			float2 appendResult46 = (float2(_DetailMaskTile , _DetailMaskTile));
			float4 triplanar40 = TriplanarSampling40( _TopTexture2, ase_worldPos, ase_worldNormal, _DetailMapFalloff, appendResult46, 1.0, 0 );
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float dotResult31 = dot( ase_worldNormal , ase_worldlightDir );
			float temp_output_36_0 = saturate( ( ( dotResult31 + _HighlightOffset ) / _HighlightlSharpness ) );
			float2 appendResult11 = (float2(_ShadowTextureTile , _ShadowTextureTile));
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float3 ase_vertexNormal = mul( unity_WorldToObject, float4( ase_worldNormal, 0 ) );
			ase_vertexNormal = normalize( ase_vertexNormal );
			float4 triplanar21 = TriplanarSampling21( _TopTexture0, ase_vertex3Pos, ase_vertexNormal, _ShadowTextureFalloff, appendResult11, 1.0, 0 );
			float4 color20 = IsGammaSpace() ? float4(1,1,1,0) : float4(1,1,1,0);
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float fresnelNdotV59 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode59 = ( 0.0 + _FScale * pow( 1.0 - fresnelNdotV59, _FPower ) );
			float temp_output_60_0 = pow( fresnelNode59 , _FContrast );
			float dotResult3 = dot( ase_worldNormal , ase_worldlightDir );
			float temp_output_8_0 = saturate( ( ( dotResult3 + _BaseCellOffset ) / _BaseCellSharpness ) );
			float4 lerpResult22 = lerp( triplanar21 , color20 , ( temp_output_60_0 + temp_output_8_0 ));
			float4 lerpResult67 = lerp( ( _BaseColor * lerpResult22 * saturate( ( pow( temp_output_8_0 , 15.0 ) + _ShadowIntensity ) ) ) , _RimLightColor , saturate( ( saturate( temp_output_60_0 ) * ( temp_output_8_0 + 0.2 ) ) ));
			float4 lerpResult37 = lerp( lerpResult67 , _HighlightColor , temp_output_36_0);
			float2 appendResult53 = (float2(_InkSpotsTile , _InkSpotsTile));
			float4 triplanar54 = TriplanarSampling54( _TopTexture3, ase_vertex3Pos, ase_vertexNormal, _DetailMapFalloff, appendResult53, 1.0, 0 );
			o.Albedo = ( saturate( ( ( triplanar39 + _DetailMapIntensity ) + triplanar40 + ( temp_output_36_0 * 0.5 ) ) ) * lerpResult37 * saturate( ( triplanar54.x + 0.2 ) ) ).xyz;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float4 tSpace0 : TEXCOORD1;
				float4 tSpace1 : TEXCOORD2;
				float4 tSpace2 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18921
207.2;73.6;630;371;-549.9411;1639.679;6.6649;True;False
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;2;-927.8696,422.8031;Inherit;True;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;1;-914.8696,137.8032;Inherit;True;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;5;-744.8697,615.8035;Inherit;False;Property;_BaseCellOffset;Base Cell Offset;4;0;Create;False;0;0;0;False;0;False;0.01176471;-0.35;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;3;-661.7089,216.8031;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;4;-370.8693,332.8031;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-633.8697,711.804;Inherit;False;Property;_BaseCellSharpness;Base Cell Sharpness;8;0;Create;False;0;0;0;False;0;False;0.1497647;0.255;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;7;-228.093,473.6213;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-847.3942,-264.2266;Inherit;False;Property;_FScale;F Scale;18;0;Create;True;0;0;0;False;0;False;25.13;0.42;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-806.4568,-136.791;Inherit;False;Property;_FPower;F Power;19;0;Create;True;0;0;0;False;0;False;10.15;7.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;30;470.9331,696.6698;Inherit;True;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;29;483.9332,411.6693;Inherit;True;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;9;-1141.233,-898.0074;Inherit;False;Property;_ShadowTextureTile;Shadow Texture Tile;6;0;Create;False;0;0;0;False;0;False;2;0.71;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;31;737.0938,490.6693;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;8;-24.86934,345.8031;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;59;-571.4568,-270.791;Inherit;True;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;61;-407.4568,-6.791046;Inherit;False;Property;_FContrast;F Contrast;20;0;Create;True;0;0;0;False;0;False;0.68;1.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;520.9792,907.3966;Inherit;False;Property;_HighlightOffset;Highlight Offset;5;0;Create;False;0;0;0;False;0;False;-0.492891;-0.65;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;11;-817.5536,-893.1437;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-1151.554,-745.1443;Inherit;False;Property;_ShadowTextureFalloff;Shadow Texture Falloff;7;0;Create;False;0;0;0;False;0;False;18.08;17.29;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;33;1027.933,606.6697;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;764.933,985.6698;Inherit;False;Property;_HighlightlSharpness;Highlight Sharpness;9;0;Create;False;0;0;0;False;0;False;0.01;0.01;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;25;772.1183,-50.78718;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;15;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;650.5497,157.9238;Inherit;False;Property;_ShadowIntensity;Shadow Intensity;11;0;Create;True;0;0;0;False;0;False;0.5294118;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;1264.356,-1304.833;Inherit;False;Property;_DetailMapTile;Detail Map Tile;13;0;Create;True;0;0;0;False;0;False;2.5;2.62;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;60;-184.4568,-194.791;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;42;1129.154,-1204.733;Inherit;False;Property;_DetailMaskTile;Detail Mask Tile;14;0;Create;True;0;0;0;False;0;False;2;-1.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;45;1521.756,-1285.333;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;1036.774,41.73425;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;64;381.1473,-1142.049;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;62;268.5432,-60.79102;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;63;343.1597,-1257.027;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TriplanarNode;21;-658.7637,-889.0156;Inherit;True;Spherical;Object;False;Top Texture 0;_TopTexture0;white;3;None;Mid Texture 0;_MidTexture0;white;-1;None;Bot Texture 0;_BotTexture0;white;-1;None;Shadow Textrure;Tangent;10;0;SAMPLER2D;;False;5;FLOAT;1;False;1;SAMPLER2D;;False;6;FLOAT;0;False;2;SAMPLER2D;;False;7;FLOAT;0;False;9;FLOAT3;0,0,0;False;8;FLOAT;1;False;3;FLOAT2;1,1;False;4;FLOAT;1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;52;1399.602,-691.3265;Inherit;False;Property;_InkSpotsTile;Ink Spots Tile;17;0;Create;True;0;0;0;False;0;False;1;0.21;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;35;1170.709,747.488;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;43;1166.856,-1014.932;Inherit;False;Property;_DetailMapFalloff;Detail Map Falloff;15;0;Create;True;0;0;0;False;0;False;20;21.15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;20;-753.7638,-680.6156;Inherit;False;Constant;_White;White;5;0;Create;True;0;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;53;1637.31,-693.336;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TriplanarNode;39;1903.8,-1224.957;Inherit;True;Spherical;World;False;Top Texture 1;_TopTexture1;white;1;None;Mid Texture 1;_MidTexture1;white;-1;None;Bot Texture 1;_BotTexture1;white;-1;None;Detail Map;Tangent;10;0;SAMPLER2D;;False;5;FLOAT;1;False;1;SAMPLER2D;;False;6;FLOAT;0;False;2;SAMPLER2D;;False;7;FLOAT;0;False;9;FLOAT3;0,0,0;False;8;FLOAT;1;False;3;FLOAT2;1,1;False;4;FLOAT;1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;46;1528.256,-1143.633;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;44;2160.056,-1397.134;Inherit;False;Property;_DetailMapIntensity;Detail Map Intensity;16;0;Create;True;0;0;0;False;0;False;0.2239311;0.571;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;28;1249.787,89.07126;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;23;745.1279,-855.3054;Inherit;False;Property;_BaseColor;Base Color;10;0;Create;True;0;0;0;False;0;False;1,0.5613208,0.5613208,0;0.06150279,1,0,1.001008;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;22;737.5407,-462.6561;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;608.2487,-1179.359;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;36;1373.933,619.6697;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;1345.142,-372.2398;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;2276.572,-383.9178;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;68;1506.128,-83.6944;Inherit;False;Property;_RimLightColor;Rim Light Color;21;0;Create;True;0;0;0;False;0;False;0.6560061,1,0.6556604,0;0.4497151,0.5186898,0.5849056,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TriplanarNode;54;1843.31,-773.336;Inherit;True;Spherical;Object;False;Top Texture 3;_TopTexture3;white;0;None;Mid Texture 3;_MidTexture3;white;-1;None;Bot Texture 3;_BotTexture3;white;-1;None;Triplanar Sampler;Tangent;10;0;SAMPLER2D;;False;5;FLOAT;1;False;1;SAMPLER2D;;False;6;FLOAT;0;False;2;SAMPLER2D;;False;7;FLOAT;0;False;9;FLOAT3;0,0,0;False;8;FLOAT;1;False;3;FLOAT2;1,1;False;4;FLOAT;1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TriplanarNode;40;1904.963,-992.7529;Inherit;True;Spherical;World;False;Top Texture 2;_TopTexture2;white;2;None;Mid Texture 2;_MidTexture2;white;-1;None;Bot Texture 2;_BotTexture2;white;-1;None;Detail Mask;Tangent;10;0;SAMPLER2D;;False;5;FLOAT;1;False;1;SAMPLER2D;;False;6;FLOAT;0;False;2;SAMPLER2D;;False;7;FLOAT;0;False;9;FLOAT3;0,0,0;False;8;FLOAT;1;False;3;FLOAT2;1,1;False;4;FLOAT;1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;66;763.2634,-1180.794;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;47;2560.455,-1144.933;Inherit;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ColorNode;38;1545.288,161.86;Inherit;False;Property;_HighlightColor;Highlight Color;12;0;Create;True;0;0;0;False;0;False;0.6525454,0.9659855,0.9811321,0;0.09066818,1,0,1.001008;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;67;1829.83,-380.2673;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;55;2307.648,-759.9161;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;48;2776.891,-862.8031;Inherit;True;3;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SaturateNode;56;2495.648,-739.9161;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;50;3063.337,-802.6511;Inherit;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.LerpOp;37;2234.182,-46.19854;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;3324.962,-446.3682;Inherit;True;3;3;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;3680.89,-265.7612;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Crayon;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;True;2;False;-1;255;False;-1;255;False;-1;7;False;-1;3;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;1;0
WireConnection;3;1;2;0
WireConnection;4;0;3;0
WireConnection;4;1;5;0
WireConnection;7;0;4;0
WireConnection;7;1;6;0
WireConnection;31;0;29;0
WireConnection;31;1;30;0
WireConnection;8;0;7;0
WireConnection;59;2;57;0
WireConnection;59;3;58;0
WireConnection;11;0;9;0
WireConnection;11;1;9;0
WireConnection;33;0;31;0
WireConnection;33;1;32;0
WireConnection;25;0;8;0
WireConnection;60;0;59;0
WireConnection;60;1;61;0
WireConnection;45;0;41;0
WireConnection;45;1;41;0
WireConnection;26;0;25;0
WireConnection;26;1;27;0
WireConnection;64;0;8;0
WireConnection;62;0;60;0
WireConnection;62;1;8;0
WireConnection;63;0;60;0
WireConnection;21;3;11;0
WireConnection;21;4;10;0
WireConnection;35;0;33;0
WireConnection;35;1;34;0
WireConnection;53;0;52;0
WireConnection;53;1;52;0
WireConnection;39;3;45;0
WireConnection;39;4;43;0
WireConnection;46;0;42;0
WireConnection;46;1;42;0
WireConnection;28;0;26;0
WireConnection;22;0;21;0
WireConnection;22;1;20;0
WireConnection;22;2;62;0
WireConnection;65;0;63;0
WireConnection;65;1;64;0
WireConnection;36;0;35;0
WireConnection;24;0;23;0
WireConnection;24;1;22;0
WireConnection;24;2;28;0
WireConnection;49;0;36;0
WireConnection;54;3;53;0
WireConnection;54;4;43;0
WireConnection;40;3;46;0
WireConnection;40;4;43;0
WireConnection;66;0;65;0
WireConnection;47;0;39;0
WireConnection;47;1;44;0
WireConnection;67;0;24;0
WireConnection;67;1;68;0
WireConnection;67;2;66;0
WireConnection;55;0;54;1
WireConnection;48;0;47;0
WireConnection;48;1;40;0
WireConnection;48;2;49;0
WireConnection;56;0;55;0
WireConnection;50;0;48;0
WireConnection;37;0;67;0
WireConnection;37;1;38;0
WireConnection;37;2;36;0
WireConnection;51;0;50;0
WireConnection;51;1;37;0
WireConnection;51;2;56;0
WireConnection;0;0;51;0
ASEEND*/
//CHKSM=49986838A5ECF96F91EE4F7B3E17447842672C3F