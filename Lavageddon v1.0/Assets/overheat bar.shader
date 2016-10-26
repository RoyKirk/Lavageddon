// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32716,y:32678,varname:node_4795,prsc:2|emission-3131-OUT,clip-9066-OUT;n:type:ShaderForge.SFN_TexCoord,id:8608,x:31752,y:32825,varname:node_8608,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:8296,x:32133,y:32735,varname:node_8296,prsc:2|A-9788-OUT,B-2680-OUT;n:type:ShaderForge.SFN_Slider,id:7559,x:31611,y:32716,ptovrint:False,ptlb:node_7559,ptin:_node_7559,varname:node_7559,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:100,max:100;n:type:ShaderForge.SFN_OneMinus,id:2680,x:31905,y:32825,varname:node_2680,prsc:2|IN-8608-V;n:type:ShaderForge.SFN_RemapRange,id:9788,x:31934,y:32654,varname:node_9788,prsc:2,frmn:0,frmx:100,tomn:0,tomx:1|IN-7559-OUT;n:type:ShaderForge.SFN_Floor,id:9066,x:32297,y:32735,varname:node_9066,prsc:2|IN-8296-OUT;n:type:ShaderForge.SFN_Color,id:576,x:31690,y:32490,ptovrint:False,ptlb:node_576,ptin:_node_576,varname:node_576,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:2857,x:31534,y:32374,ptovrint:False,ptlb:node_2857,ptin:_node_2857,varname:node_2857,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9034482,c2:1,c3:0,c4:1;n:type:ShaderForge.SFN_Lerp,id:3131,x:32026,y:32445,varname:node_3131,prsc:2|A-2857-RGB,B-576-RGB,T-9788-OUT;proporder:7559-576-2857;pass:END;sub:END;*/

Shader "Shader Forge/overheat bar" {
    Properties {
        _node_7559 ("node_7559", Range(0, 100)) = 100
        _node_576 ("node_576", Color) = (1,0,0,1)
        _node_2857 ("node_2857", Color) = (0.9034482,1,0,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _node_7559;
            uniform float4 _node_576;
            uniform float4 _node_2857;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float node_9788 = (_node_7559*0.01+0.0);
                float node_2680 = (1.0 - i.uv0.g);
                float node_9066 = floor((node_9788+node_2680));
                clip(node_9066 - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = lerp(_node_2857.rgb,_node_576.rgb,node_9788);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
