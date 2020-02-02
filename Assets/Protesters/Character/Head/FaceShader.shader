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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _FaceVariant;
			float _FaceExpression;
			float4 _HairColor;
			float4 _SkinColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				float2 sUV = v.uv + float2(_FaceVariant, _FaceExpression);
                o.uv = TRANSFORM_TEX(sUV, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 lookup = tex2D(_MainTex, i.uv);
				float4 skin = lerp(_SkinColor, _HairColor, lookup.r);
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
