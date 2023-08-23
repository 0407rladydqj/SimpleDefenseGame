Shader "Shaders/SimpleShader"//���̴��� ���Ǵ� ��ο� �̸�
{
    Properties
    {
        _Color("Base Color",color) = (1,1,1,1)
    }

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #include"UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            fixed4 _Color;//fixed4 ������ 4���� ���ڸ� ������ �� �ִ� Ÿ��
            struct vertexInput
            {
                float3 posOnObjSpace : POSITION;
            };

            struct fragmentInput 
            {
                float4 posOnClipSpace : SV_POSITION;
            };

            fragmentInput vert(vertexInput input)
            {
                float4 posOnClipSpace = UnityObjectToClipPos(input.posOnObjSpace);

                fragmentInput output;
                output.posOnClipSpace = posOnClipSpace;

                return output;
            }

            fixed4 frag(fragmentInput input) : SV_TARGET
            {
                return _Color;
            }
            ENDCG
        }
    }
}
