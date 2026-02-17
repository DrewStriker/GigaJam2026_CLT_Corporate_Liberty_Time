Shader "Custom/BillboardY"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Size ("Size", Float) = 1
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent" "RenderType"="Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Size;

            struct appdata
            {
                float3 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;

                // Pivot do objeto no mundo
                float3 worldPivot = mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;

                // Direção até a câmera
                float3 toCam = _WorldSpaceCameraPos - worldPivot;

                // trava Y (billboard vertical)
                toCam.y = 0;
                toCam = normalize(toCam);

                float3 up = float3(0, 1, 0);
                float3 right = normalize(cross(up, toCam));

                float3 offset =
                    right * v.vertex.x * _Size +
                    up * v.vertex.y * _Size;

                float3 finalPos = worldPivot + offset;

                o.pos = UnityWorldToClipPos(finalPos);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDHLSL
        }
    }
}