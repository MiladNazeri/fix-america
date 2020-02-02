Shader "fix-america/protester/FaceShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_FaceVariant ("Face Type", Int) = 0
		_FaceExpression("Face Expression", Int) = 0
		_SkinColor ("Skin Color", Color) = (0.0, 1.0, 0.0, 0.0)
		_HairColor ("Hair Color", Color) = (0.0, 0.0, 0.0, 0.0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
            };

			UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(float, _FaceVariant)
				UNITY_DEFINE_INSTANCED_PROP(float, _FaceExpression)
                UNITY_DEFINE_INSTANCED_PROP(float4, _SkinColor)
                UNITY_DEFINE_INSTANCED_PROP(float4, _HairColor)
            UNITY_INSTANCING_BUFFER_END(Props)

            sampler2D _MainTex;
            float4 _MainTex_ST;
			//float _FaceVariant;
			//float _FaceExpression;
			//float4 _HairColor;
			//float4 _SkinColor;

            v2f vert (appdata v)
            {
                v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                o.vertex = UnityObjectToClipPos(v.vertex);
				float2 sUV = v.uv + float2(UNITY_ACCESS_INSTANCED_PROP(Props, _FaceVariant), UNITY_ACCESS_INSTANCED_PROP(Props, _FaceExpression));
                o.uv = TRANSFORM_TEX(sUV, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				UNITY_SETUP_INSTANCE_ID(i);
                // sample the texture
                fixed4 lookup = tex2D(_MainTex, i.uv);
				float4 skin = lerp(UNITY_ACCESS_INSTANCED_PROP(Props, _SkinColor), UNITY_ACCESS_INSTANCED_PROP(Props, _HairColor), lookup.r);
				float4 black = lerp(skin, float4(0.0, 0.0, 0.0, 1.0), lookup.g);
				float4 white = lerp(black, float4(1.0, 1.0, 1.0, 1.0), lookup.b);

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, lookup);
                return white;
            }
            ENDCG
        }
    }
}
