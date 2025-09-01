Shader "Unlit/LanternMask_UI_Corrected"
{
    Properties
    {
        _MaskPosition("Mask Position", Vector) = (0.5, 0.5, 0, 0)
        _MaskRadius("Mask Radius", Float) = 0.2
        _AspectRatio("Aspect Ratio", Float) = 1.0
        _Color("Color", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Overlay" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

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
                float4 screenPos : TEXCOORD1;
            };

            float4 _MaskPosition;
            float _MaskRadius;
            float _AspectRatio;
            float4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.screenPos = o.vertex;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 screenUV = (i.screenPos.xy - float2(0.0, 0.5)) / i.screenPos.w;
                screenUV = screenUV * 0.5 + 0.5; // 0~1

                // Corrige o aspect ratio :
                float2 delta = screenUV - _MaskPosition.xy;
                delta.x *= _AspectRatio;

                float dist = length(delta);

                // Adiciona uma área de transição suave
                float transitionWidth = 0.075; // Controla a largura da transição (ajuste conforme necessário)
                float alpha = smoothstep(_MaskRadius - transitionWidth, _MaskRadius, dist);

                // Aplica o alpha à cor
                fixed4 col = _Color;
                col.a *= alpha;
                return col;
            }
            ENDCG
        }
    }
}
