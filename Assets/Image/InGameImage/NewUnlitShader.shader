Shader "UI/GradientShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)  // �⺻ ����
    }
        SubShader
    {
        Tags { "Queue" = "Transparent" }
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
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // x ��ǥ�� �������� ���� ����
                float alpha = i.uv.x;
                return fixed4(_Color.rgb, alpha * _Color.a); // ���� ä�� ����
            }
            ENDCG
        }
    }
}
