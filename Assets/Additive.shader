Shader "Custom/Additive"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _AddTex ("Additive Texture", 2D) = "white" {}
        _AddArea ("Additive Area", Vector) = (0, 0, 1, 1)
    }
    SubShader
    {
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

            sampler2D _MainTex;
            sampler2D _AddTex;
            float4 _MainTex_ST;
            float4 _AddTex_ST;
            float4 _AddArea;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                if (i.uv.x >= _AddArea.x && i.uv.y >= _AddArea.y && i.uv.x <= _AddArea.z && i.uv.y <= _AddArea.w)
                {
                    float2 addUV = (i.uv - _AddArea.xy) / (_AddArea.zw - _AddArea.xy);
                    fixed4 addCol = tex2D(_AddTex, addUV);
                    col += addCol;
                }
                return col;
            }
            ENDCG
        }
    }
}
