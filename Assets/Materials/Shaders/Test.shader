Shader "Unlit/Test"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color: COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            

            float rand(float3 co){
                return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 56.787))) * 43758.5453);
            }
            
            v2f vert (appdata v)
            {
                v2f o;
                
                /*
                //original start
                float offsetY = sin(v.vertex.x + _Time.y);
                v.vertex.y += offsetY;
                o.color = float4(offsetY, offsetY*5, 1, 1);
                //original end
                */
                
                //original start
                float rnd = rand(float3(v.vertex.x,0,v.vertex.z));
                float offsetY =  rnd;//sin(v.vertex.x + rand(float4(0,0,0,0)) ) + sin(v.vertex.z + rand(float4(0,0,0,0)) );
                v.vertex.y += offsetY;
                o.color = float4(offsetY, offsetY*5, 1, 1);

                //original end
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = i.color;//tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
