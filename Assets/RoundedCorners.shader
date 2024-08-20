Shader "Custom/RoundedCorners"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _CornerRadius ("Corner Radius", Float) = 10
        _BorderWidth ("Border Width", Float) = 0
        _BorderColor ("Border Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
            };

            fixed4 _Color;
            fixed4 _BorderColor;
            float _CornerRadius;
            float _BorderWidth;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.worldPosition = IN.vertex;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                return OUT;
            }

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            fixed4 frag(v2f IN) : SV_Target
            {
                float2 uv = IN.texcoord;
                float2 size = _MainTex_TexelSize.zw;

                float2 center = size * 0.5;
                float2 d = abs(uv * size - center) + center - size;
                float distance = length(max(d, 0));

                float alpha = 1 - smoothstep(_CornerRadius - 1, _CornerRadius + 1, distance);

                fixed4 color = tex2D(_MainTex, IN.texcoord) * IN.color;
                color.a *= alpha;

                // Apply border
                if (_BorderWidth > 0)
                {
                    float borderAlpha = 1 - smoothstep(_CornerRadius - _BorderWidth - 1, _CornerRadius - _BorderWidth + 1, distance);
                    color = lerp(_BorderColor, color, borderAlpha);
                }

                return color;
            }
            ENDCG
        }
    }
}