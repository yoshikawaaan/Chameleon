Shader "Masked" {
    SubShader {
        Tags { "RenderType"="Opaque" "Queue"="Geometry+2"}
        //------------------------------------------------
        Pass {
            Stencil
            {
                Ref 10
                Comp Equal
                ZFail IncrSat
            }
        
            Cull Back
            Zwrite Off
            ZTest GEqual
            ColorMask 0
            
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
            struct appdata {
                float4 vertex : POSITION;
            };
            struct v2f {
                float4 pos : SV_POSITION;
            };
            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            half4 frag(v2f i) : SV_Target {
                return half4(0,0,1,1);
            }
            ENDCG
        }
        
        Pass {
            Stencil
            {
                Ref 11
                Comp Equal
                ZFail IncrSat
            }
        
            //Cull Front
            Zwrite Off
            ZTest LEqual
            //ColorMask 0
            
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
            struct appdata {
                float4 vertex : POSITION;
            };
            struct v2f {
                float4 pos : SV_POSITION;
            };
            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            half4 frag(v2f i) : SV_Target {
                return half4(1,0,0,1);
            }
            ENDCG
        }
        
        Pass {
            Stencil
            {
                Ref 10
                Comp Equal
                ZFail DecrSat
            }
        
            Cull Back
            Zwrite Off
            ZTest LEqual
            ColorMask 0
            
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
            struct appdata {
                float4 vertex : POSITION;
            };
            struct v2f {
                float4 pos : SV_POSITION;
            };
            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            half4 frag(v2f i) : SV_Target {
                return half4(0,1,0,1);
            }
            ENDCG
        }
    }
}