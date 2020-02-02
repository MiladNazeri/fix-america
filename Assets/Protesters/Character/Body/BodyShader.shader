Shader "fix-america/protester/BodyShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Variant ("Clothes Type", Int) = 0
		_SkinColor ("Skin Color", Color) = (0.0, 1.0, 0.0, 0.0)
		_ColorA ("Color A", Color) = (0.0, 1.0, 0.0, 0.0)
		_ColorC ("Color C", Color) = (0.0, 0.0, 0.0, 0.0)
		_ColorB ("Color B", Color) = (0.0, 0.0, 0.0, 0.0)
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
                UNITY_DEFINE_INSTANCED_PROP(float, _Variant)
				UNITY_DEFINE_INSTANCED_PROP(float4, _SkinColor)
                UNITY_DEFINE_INSTANCED_PROP(float4, _ColorA)
                UNITY_DEFINE_INSTANCED_PROP(float4, _ColorB)
				UNITY_DEFINE_INSTANCED_PROP(float4, _ColorC)
            UNITY_INSTANCING_BUFFER_END(Props)

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                o.vertex = UnityObjectToClipPos(v.vertex);
				float2 sUV = v.uv + float2(0.0, UNITY_ACCESS_INSTANCED_PROP(Props, _Variant));
                o.uv = TRANSFORM_TEX(sUV, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				UNITY_SETUP_INSTANCE_ID(i);
                // sample the texture
                fixed4 lookup = tex2D(_MainTex, i.uv);
				float4 a = lerp(UNITY_ACCESS_INSTANCED_PROP(Props, _SkinColor), UNITY_ACCESS_INSTANCED_PROP(Props, _ColorA), lookup.r);
				float4 b = lerp(a, UNITY_ACCESS_INSTANCED_PROP(Props, _ColorB), lookup.g);
				float4 c = lerp(b, UNITY_ACCESS_INSTANCED_PROP(Props, _ColorC), lookup.b);

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, lookup);
                return c;
            }
            ENDCG
        }
    }
}
