Shader "Unlit/AGORAVAI"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" { }
        _SphereCenter("Sphere Center", Vector) = (0, 0, 0, 0)
        _SphereRadius("Sphere Radius", Float) = 5.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Declaração de variáveis
            float4 _SphereCenter; // Centro da esfera
            float _SphereRadius; // Raio da esfera
            sampler2D _MainTex; // Textura base

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            // Função do vértice
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = v.uv;
                return o;
            }

            // Função do fragmento
            fixed4 frag(v2f i) : SV_Target
            {
                // Calcula a distância do ponto no mundo até o centro da esfera
                float dist = distance(i.worldPos, _SphereCenter.xyz);
                
                // Cria uma máscara de suavização para a transição entre cor e preto e branco
                float mask = smoothstep(_SphereRadius, _SphereRadius - 1.0, dist);

                // Obtém a cor da textura
                fixed4 originalColor = tex2D(_MainTex, i.uv);

                // Converte a cor para escala de cinza
                float gray = dot(originalColor.rgb, fixed3(0.3, 0.59, 0.11));
                fixed3 grayColor = fixed3(gray, gray, gray);

                // Interpola entre a cor original e a cor em escala de cinza
                fixed3 finalColor = lerp(originalColor.rgb, grayColor, mask);

                return fixed4(finalColor, originalColor.a);
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
