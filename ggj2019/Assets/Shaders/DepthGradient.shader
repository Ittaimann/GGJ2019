Shader "Custom/DepthGradient"
{
    Properties
    {
        _mainColor("main color",Color) = (1,1,1,1)  
        _Color1 ("Color1", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (1,1,1,1)
        _farPlaneRange("far plane range", range(0,10)) = 1
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _CameraDepthTexture;
        struct Input
        {
            float4 screenPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color1,_Color2;
        fixed _farPlaneRange;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {

            float2 uv = IN.screenPos.xy/IN.screenPos.w;
            float depth = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,uv));
            // Albedo comes from a texture tinted by color
            o.Albedo = lerp(_Color1,_Color2,depth*_farPlaneRange);
            o.Metallic = _Metallic;
            o.Smoothness=  _Glossiness; 
            
        }
        ENDCG
    }
    FallBack "Diffuse"
}
