Shader "Unlit/Texture"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * 100;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4((int)i.uv.x & (int)i.uv.y, ((int)i.uv.x & 15) / 15.0, ((int)i.uv.y & 15) / 15.0, 0.0);
            }
            ENDCG
        }
    }
}
