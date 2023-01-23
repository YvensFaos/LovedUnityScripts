Shader "RRShaders/SceneTransition"
{
	Properties
	{
	    _MainTex ("Texture", 2D) = "white" {}

	    /*
	    0: Slide in from left to right - Slide out from left to right
	    1: Slide in from bottom to top
	    2: Vertical blinds
	    3: Horizontal blinds
	    4: Flash
	    5: That's all folks!
	    6: Reversed That's all folks!
	    */
	    _Mode("Mode", Int) = 0
	    _FocalPoint("Focal Point", Vector) = (0, 0, 0, 0)
	    _InvertMode("Invert Mode (not working yet)", Int) = 0
	    _Color("Color", Color) = (1, 1, 1, 1)
	    _Done("Done", Int) = 0

	    _Phase ("Phase", Int) = 0
	    _Progress("Progress", Float) = 0.0

	    _BlindSize ("Blind Size", Float) = 64.0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always


		Pass
		{
			CGPROGRAM
// Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
#pragma exclude_renderers d3d11 gles
// Upgrade NOTE: excluded shader from DX11 because it uses wrong array syntax (type[size] name)
#pragma exclude_renderers d3d11
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			uniform float4 _MainTex_TexelSize;

			int _Mode;
			int _InvertMode;
			float _BlindSize;
			float4 _FocalPoint;
			fixed4 _Color;
			int _Done;
			float _Phase;
		    float _Progress;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			bool distanceBetweenSmallerThanOrEqualTo(float2 position1, float2 position2, float distance) {
			    float x = position1[0] - position2[0];
			    #if UNITY_UV_STARTS_AT_TOP
			    float y = position1[1] - position2[1];
			    #else
			    float y = (1.0f - position1[1]) - (1.0f - position2[1]);
			    #endif
			    return x*x + y*y <= distance * distance;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				// Skip everything else
				if(_Done == 1) {
				    return col;
				}

				float y = i.vertex[1];
				#if UNITY_UV_STARTS_AT_TOP


				#else
				y = 1 - y;
				#endif

				// Slide in and out curtain
				if(_Mode == 0) {
				    float width = _MainTex_TexelSize.z;
				    if(_Phase == 0) {
                        if(i.vertex[0] < _Progress * width) {
                            col.rgb = _Color;
                        }
                    }
                    if(_Phase == 1) {
                        if(i.vertex[0] > _Progress * width) {
                            col.rgb = _Color;
                        }
                    }
				}
				if(_Mode == 1) {
				    float height = _MainTex_TexelSize.w;
				    if(_Phase == 0) {
				        #if UNITY_UV_STARTS_AT_TOP
				        if(i.vertex[1] < _Progress * height) {
                            col.rgb = _Color;
                        }
                        #else
                        if(i.vertex[1] > (1 - _Progress) * height) {
                            col.rgb = _Color;
                        }
                        #endif
                    }
                    if(_Phase == 1) {
                        #if UNITY_UV_STARTS_AT_TOP
                        if(i.vertex[1] > _Progress * height) {
                            col.rgb = _Color;
                        }
                        #else
                        if(i.vertex[1] < (1 - _Progress) * height) {
                            col.rgb = _Color;
                        }
                        #endif
                    }
				}

				// Horizontal blinds
				if(_Mode == 2) {
				    if(_Phase == 0) {
                        if(fmod(i.vertex[0], _BlindSize) < _Progress * _BlindSize) {
                            col.rgb = _Color;
                        }
                    }
                    if(_Phase == 1) {
                        if(fmod(i.vertex[0], _BlindSize) > _Progress * _BlindSize) {
                            col.rgb = _Color;
                        }
                    }
				}

				// Vertical blinds
				if(_Mode == 3) {
				    if(_Phase == 0) {
                        if(fmod(y, _BlindSize) < _Progress * _BlindSize) {
                            col.rgb = _Color;
                        }
                    }
                    if(_Phase == 1) {
                        if(fmod(y, _BlindSize) > _Progress * _BlindSize) {
                            col.rgb = _Color;
                        }
                    }
				}

				// Flash
				if(_Mode == 4) {
				    fixed4 ColorWhite = fixed4(1, 1, 1, 1);
				    if(_Phase == 0) {
				        col.rgba += _Progress * ColorWhite;
                    }
                    if(_Phase == 1) {
                        col.rgba += (1-_Progress) * ColorWhite;
                    }
				}

				if(_Mode == 5) {
				    float height = _MainTex_TexelSize.w;
				    float width = _MainTex_TexelSize.z;

				    #if UNITY_UV_STARTS_AT_TOP
				    float2 focalPoint = float2(_FocalPoint[0], height - _FocalPoint[1]);
				    #else
				    float2 focalPoint = _FocalPoint;
				    #endif

				    float limit = width > height ? width : height;
				    if(_Phase == 0) {
				        if(!distanceBetweenSmallerThanOrEqualTo(i.vertex, focalPoint, (1-_Progress) * limit)) {
				            col.rgba = _Color;
				        }
                    }

                    if(_Phase == 1) {
				        if(!distanceBetweenSmallerThanOrEqualTo(i.vertex, focalPoint, _Progress * limit)) {
				            col.rgba = _Color;
				        }
                    }
				}

				if(_Mode == 6) {
				    float height = _MainTex_TexelSize.w;
				    float width = _MainTex_TexelSize.z;

				    #if UNITY_UV_STARTS_AT_TOP
				    float2 focalPoint = float2(_FocalPoint[0], height - _FocalPoint[1]);
				    #else
				    float2 focalPoint = _FocalPoint;
				    #endif

				    float limit = width > height ? width : height;
				    if(_Phase == 0) {
				        if(distanceBetweenSmallerThanOrEqualTo(i.vertex, focalPoint, _Progress * limit)) {
				            col.rgba = _Color;
				        }
                    }

                    if(_Phase == 1) {
				        if(distanceBetweenSmallerThanOrEqualTo(i.vertex, focalPoint, (1 - _Progress) * limit)) {
				            col.rgba = _Color;
				        }
                    }
				}
				return col;
			}
			ENDCG
		}
	}
}
