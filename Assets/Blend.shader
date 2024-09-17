Shader "Custom/Blend"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlendTex ("Blend Texture", 2D) = "white" {}
        _BlendFactor ("Blend Factor", Range(0, 1)) = 0.5
        _BlendArea ("Blend Area", Vector) = (0, 0, 1, 1)
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
            sampler2D _BlendTex;
            float4 _MainTex_ST;
            float4 _BlendTex_ST;
            float _BlendFactor;
            float4 _BlendArea;

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
                if (i.uv.x >= _BlendArea.x && i.uv.y >= _BlendArea.y && i.uv.x <= _BlendArea.z && i.uv.y <= _BlendArea.w)
                {
                    float2 blendUV = (i.uv - _BlendArea.xy) / (_BlendArea.zw - _BlendArea.xy);
                    fixed4 blendCol = tex2D(_BlendTex, blendUV);
                    col = lerp(col, blendCol, blendCol.a);
                }
                return col;
            }
            ENDCG
        }
    }
}
