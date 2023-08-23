Shader "Shaders/SimpleShader"//셰이더가 사용되는 경로와 이름
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

            fixed4 _Color;//fixed4 나열된 4개의 숫자를 저장할 수 있는 타입
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
