Shader "LSQ/Terrain/Water/River"
{
    Properties
    {
        [Header(Depth Gradient)]
        _DepthShallowColor("Depth Shallow Color", Color) = (0.325, 0.807, 0.971, 0.725)
		_DepthDeepColor("Depth Deep Color", Color) = (0.086, 0.407, 1, 0.749)
        _DepthMaxDistance("Depth Maximum Distance", Float) = 1

        [Header(Foam)]
        _FoamColor("Foam Color", Color) = (1,1,1,1)
        _FoamMaxDistance("Foam Maximum Distance", Float) = 0.2
        _SurfaceNoise("Surface Noise", 2D) = "white" {}
		_SurfaceNoiseCutoff("Surface Noise Cutoff", Range(0, 1)) = 0.777
        _SurfaceDistortionScale("Surface Distortion Scale", Range(0, 1)) = 0.27

        [Header(Distortion)]
        _SurfaceDistortion("Surface Distortion", 2D) = "white" {}	

        [Header(Caustics)]
        _CausticsColor("Color", Color) = (1,1,1,1)
        _CausticsTex ("Texture", 2D) = "black"{}
        _CausticsScale ("Scale", float) = 1.0
        _CausticsDistortionScale ("Caustics Distortion Scale", Range(0.0, 1.0)) = 0.5
       
        [Wave]
        [Toggle]_UseWave("Use Wave", int) = 1
        _Xm ("Xm", Range(0, 1)) = 0.72
        _P ("P", Range(0, 1)) = 1
        _IntervalDistance ("IntervalDistance", float) = 5
        _DunesDistance ("DunesDistance", float) = 10
        _HeightScale ("HeightScale", float) = 0.5
        _WaveSpeed ("WaveSpeed", float) = 0
    }
    SubShader
    {
        Tags 
        { 
            "Queue" = "Transparent"
            "RenderType" = "Transparent" 
        }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass 
        {
			ZWrite On
			ColorMask 0
		}

        Pass
        {
            Tags { "LightMode"="ForwardBase" }
            ZWrite Off

            CGPROGRAM
            #pragma multi_compile_fwdbase	
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
                float2 uvCaustics : TEXCOORD3;
                float2 uvDistortion : TEXCOORD5;
                float2 uvNoise : TEXCOORD6;
            };

            sampler2D _CameraDepthTexture;

            //Depth Gradient
            float4 _DepthShallowColor;
			float4 _DepthDeepColor;
            float _DepthMaxDistance;

            //Foam
			float3 _FoamColor;
            float _FoamMaxDistance;
            sampler2D _SurfaceNoise;
			float4 _SurfaceNoise_ST;
            float _SurfaceNoiseCutoff;
            float _SurfaceDistortionScale;

            //Distortion
            sampler2D _SurfaceDistortion;
			float4 _SurfaceDistortion_ST;
            float _NormalDistortionScale;

            //Caustics
            fixed3 _CausticsColor;
            sampler2D _CausticsTex;
            float4 _CausticsTex_ST;
            float _CausticsScale;
            float _CausticsDistortionScale;

            //Wave
            int _UseWave = 1;
            float _Xm = 0.72;
            float _P = 1;
            float _IntervalDistance = 5;
            float _DunesDistance = 10;
            float _HeightScale = 0.5;
            float _WaveSpeed = 5;

            float GetWaveHeight(float2 worldPos)
            {
                float pos = worldPos.x + _WaveSpeed * _Time.y;

                float realPos = pos % (_DunesDistance + _IntervalDistance);
        
                if (realPos > _DunesDistance && realPos < _DunesDistance + _IntervalDistance) 
                {
                    return 0;
                }

                float S = 0;
                float x = realPos / _DunesDistance;
                float height;
                if (x < _Xm)
                {
                    height = _HeightScale * (1 - cos(UNITY_PI * (x - S) / (_Xm - S)));
                }
                else 
                {
                    S = 1;
                    height = _HeightScale * (_P * S + 1) * (1 - cos((UNITY_PI / (_P * S + 1)) * ((x - S) / (_Xm - S))));
                }

                return height;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.uvCaustics =  v.uv * _CausticsTex_ST.xy + _CausticsTex_ST.zw * _Time.y;
                o.uv = v.uv;
                o.uvDistortion = v.uv * _SurfaceDistortion_ST.xy + _SurfaceDistortion_ST.zw * _Time.y;
                o.uvNoise = v.uv * _SurfaceNoise_ST.xy + _SurfaceNoise_ST.zw * _Time.y;

                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                if(_UseWave)
                {
                    o.worldPos.y += GetWaveHeight(o.worldPos.xz);
                }
                
                o.vertex = mul(UNITY_MATRIX_VP, float4(o.worldPos, 1));
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 distortNoise = tex2D(_SurfaceDistortion, i.uvDistortion).xy * 2 - 1;
                float depth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos)).r;

                //depth color gradient
				float depth = LinearEyeDepth(depth01);
                float depthOffset = depth - LinearEyeDepth(i.vertex.z);//i.screenPos.w
                float depthOffset01 = saturate(depthOffset / _DepthMaxDistance);
                float4 waterColor = lerp(_DepthShallowColor, _DepthDeepColor, depthOffset01);

                //foam color
                float foamOffset01 = saturate(depthOffset / _FoamMaxDistance);
                float surfaceNoiseCutoff = foamOffset01 * _SurfaceNoiseCutoff;
				float2 noiseDistortSample = distortNoise * _SurfaceDistortionScale;
				float2 noiseUV = float2(i.uvNoise.x + noiseDistortSample.x, i.uvNoise.y + noiseDistortSample.y);
				float surfaceNoiseSample = tex2D(_SurfaceNoise, noiseUV).r;
				float surfaceNoise = smoothstep(surfaceNoiseCutoff - 0.1, surfaceNoiseCutoff + 0.1, surfaceNoiseSample);
                float3 foamColor = surfaceNoise * _FoamColor.rgb;

                //Caustics
                float2 causticsDistortSample = distortNoise * _CausticsDistortionScale;
                float2 causticsUV = float2(i.uvCaustics.x + causticsDistortSample.x, i.uvCaustics.y + causticsDistortSample.y);
                float3 causticsColor = tex2D(_CausticsTex, causticsUV) * _CausticsScale * _CausticsColor;

                return fixed4(waterColor.rgb + foamColor + causticsColor, saturate(waterColor.a + depth01));
            }
            ENDCG
        }
    }
}
