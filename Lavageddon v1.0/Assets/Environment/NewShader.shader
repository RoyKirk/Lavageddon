// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:32968,y:32650,varname:node_2865,prsc:2|diff-7604-OUT,emission-7604-OUT,voffset-1074-OUT;n:type:ShaderForge.SFN_Panner,id:2018,x:31515,y:32735,varname:node_2018,prsc:2,spu:0.15,spv:0.3|UVIN-7403-UVOUT,DIST-2245-TSL;n:type:ShaderForge.SFN_TexCoord,id:7403,x:31334,y:32689,varname:node_7403,prsc:2,uv:0;n:type:ShaderForge.SFN_Time,id:2245,x:31362,y:32925,varname:node_2245,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:6603,x:31377,y:32472,ptovrint:False,ptlb:node_6603,ptin:_node_6603,varname:node_6603,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:3,isnm:False|UVIN-2018-UVOUT;n:type:ShaderForge.SFN_Multiply,id:7300,x:31694,y:32408,varname:node_7300,prsc:2|A-6603-RGB,B-8715-RGB;n:type:ShaderForge.SFN_Tex2d,id:8715,x:31825,y:32876,ptovrint:False,ptlb:node_6603_copy,ptin:_node_6603_copy,varname:_node_6603_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-1594-UVOUT;n:type:ShaderForge.SFN_Panner,id:1594,x:31738,y:33078,varname:node_1594,prsc:2,spu:0.4,spv:0.4|UVIN-4712-UVOUT,DIST-7058-TSL;n:type:ShaderForge.SFN_Time,id:7058,x:31571,y:33241,varname:node_7058,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:4712,x:31543,y:33005,varname:node_4712,prsc:2,uv:0;n:type:ShaderForge.SFN_Clamp,id:7604,x:32366,y:32588,varname:node_7604,prsc:2|IN-324-RGB,MIN-7523-OUT,MAX-4788-OUT;n:type:ShaderForge.SFN_Vector1,id:7523,x:32141,y:32650,varname:node_7523,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:4788,x:32171,y:32718,varname:node_4788,prsc:2,v1:0.9;n:type:ShaderForge.SFN_Desaturate,id:8143,x:32641,y:32827,varname:node_8143,prsc:2|COL-7604-OUT,DES-5448-OUT;n:type:ShaderForge.SFN_Vector1,id:5448,x:32347,y:32842,varname:node_5448,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:1074,x:32783,y:32961,varname:node_1074,prsc:2|A-8143-OUT,B-755-OUT,C-1651-OUT,D-7137-OUT;n:type:ShaderForge.SFN_NormalVector,id:1651,x:32556,y:33043,prsc:2,pt:False;n:type:ShaderForge.SFN_RemapRange,id:5668,x:32045,y:32294,varname:node_5668,prsc:2,frmn:0,frmx:1,tomn:0,tomx:1.75|IN-7300-OUT;n:type:ShaderForge.SFN_Append,id:9941,x:32681,y:32374,varname:node_9941,prsc:2|A-7127-OUT,B-1851-OUT;n:type:ShaderForge.SFN_Desaturate,id:7127,x:32326,y:32374,varname:node_7127,prsc:2|COL-5668-OUT;n:type:ShaderForge.SFN_Vector1,id:1851,x:32475,y:32497,varname:node_1851,prsc:2,v1:0;n:type:ShaderForge.SFN_Tex2d,id:324,x:32850,y:32421,varname:node_324,prsc:2,tex:b5d1d43463c35c94e8fde05bf56f259e,ntxv:0,isnm:False|UVIN-9941-OUT,TEX-965-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:965,x:32681,y:32133,ptovrint:False,ptlb:node_965,ptin:_node_965,varname:node_965,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b5d1d43463c35c94e8fde05bf56f259e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:755,x:32220,y:32952,ptovrint:False,ptlb:height multiply,ptin:_heightmultiply,varname:node_755,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10000;n:type:ShaderForge.SFN_ComponentMask,id:7735,x:32305,y:33219,varname:node_7735,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-1962-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1962,x:32085,y:33219,varname:node_1962,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:3701,x:32527,y:33303,varname:node_3701,prsc:2|A-7735-OUT,B-6816-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3435,x:32110,y:33490,ptovrint:False,ptlb:node_3435,ptin:_node_3435,varname:node_3435,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Sin,id:6816,x:32277,y:33446,varname:node_6816,prsc:2|IN-3435-OUT;n:type:ShaderForge.SFN_ConstantClamp,id:6834,x:32666,y:33268,varname:node_6834,prsc:2,min:-0.5,max:1|IN-3701-OUT;n:type:ShaderForge.SFN_Multiply,id:9109,x:32827,y:33278,varname:node_9109,prsc:2|A-6834-OUT,B-7652-OUT;n:type:ShaderForge.SFN_Vector1,id:7652,x:32632,y:33481,varname:node_7652,prsc:2,v1:10;n:type:ShaderForge.SFN_Clamp01,id:7137,x:33003,y:33303,varname:node_7137,prsc:2|IN-9109-OUT;proporder:6603-8715-965-755-3435;pass:END;sub:END;*/

Shader "Shader Forge/NewShader" {
    Properties {
        _node_6603 ("node_6603", 2D) = "bump" {}
        _node_6603_copy ("node_6603_copy", 2D) = "white" {}
        _node_965 ("node_965", 2D) = "white" {}
        _heightmultiply ("height multiply", Float ) = 10000
        _node_3435 ("node_3435", Float ) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform sampler2D _node_6603; uniform float4 _node_6603_ST;
            uniform sampler2D _node_6603_copy; uniform float4 _node_6603_copy_ST;
            uniform sampler2D _node_965; uniform float4 _node_965_ST;
            uniform float _heightmultiply;
            uniform float _node_3435;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_2245 = _Time + _TimeEditor;
                float2 node_2018 = (o.uv0+node_2245.r*float2(0.15,0.3));
                float4 _node_6603_var = tex2Dlod(_node_6603,float4(TRANSFORM_TEX(node_2018, _node_6603),0.0,0));
                float4 node_7058 = _Time + _TimeEditor;
                float2 node_1594 = (o.uv0+node_7058.r*float2(0.4,0.4));
                float4 _node_6603_copy_var = tex2Dlod(_node_6603_copy,float4(TRANSFORM_TEX(node_1594, _node_6603_copy),0.0,0));
                float2 node_9941 = float2(dot(((_node_6603_var.rgb*_node_6603_copy_var.rgb)*1.75+0.0),float3(0.3,0.59,0.11)),0.0);
                float4 node_324 = tex2Dlod(_node_965,float4(TRANSFORM_TEX(node_9941, _node_965),0.0,0));
                float3 node_7604 = clamp(node_324.rgb,0.0,0.9);
                float node_3701 = (o.uv0.g*sin(_node_3435));
                v.vertex.xyz += (lerp(node_7604,dot(node_7604,float3(0.3,0.59,0.11)),1.0)*_heightmultiply*v.normal*saturate((clamp(node_3701,-0.5,1)*10.0)));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float4 node_2245 = _Time + _TimeEditor;
                float2 node_2018 = (i.uv0+node_2245.r*float2(0.15,0.3));
                float4 _node_6603_var = tex2D(_node_6603,TRANSFORM_TEX(node_2018, _node_6603));
                float4 node_7058 = _Time + _TimeEditor;
                float2 node_1594 = (i.uv0+node_7058.r*float2(0.4,0.4));
                float4 _node_6603_copy_var = tex2D(_node_6603_copy,TRANSFORM_TEX(node_1594, _node_6603_copy));
                float2 node_9941 = float2(dot(((_node_6603_var.rgb*_node_6603_copy_var.rgb)*1.75+0.0),float3(0.3,0.59,0.11)),0.0);
                float4 node_324 = tex2D(_node_965,TRANSFORM_TEX(node_9941, _node_965));
                float3 node_7604 = clamp(node_324.rgb,0.0,0.9);
                float3 emissive = node_7604;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform sampler2D _node_6603; uniform float4 _node_6603_ST;
            uniform sampler2D _node_6603_copy; uniform float4 _node_6603_copy_ST;
            uniform sampler2D _node_965; uniform float4 _node_965_ST;
            uniform float _heightmultiply;
            uniform float _node_3435;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_2245 = _Time + _TimeEditor;
                float2 node_2018 = (o.uv0+node_2245.r*float2(0.15,0.3));
                float4 _node_6603_var = tex2Dlod(_node_6603,float4(TRANSFORM_TEX(node_2018, _node_6603),0.0,0));
                float4 node_7058 = _Time + _TimeEditor;
                float2 node_1594 = (o.uv0+node_7058.r*float2(0.4,0.4));
                float4 _node_6603_copy_var = tex2Dlod(_node_6603_copy,float4(TRANSFORM_TEX(node_1594, _node_6603_copy),0.0,0));
                float2 node_9941 = float2(dot(((_node_6603_var.rgb*_node_6603_copy_var.rgb)*1.75+0.0),float3(0.3,0.59,0.11)),0.0);
                float4 node_324 = tex2Dlod(_node_965,float4(TRANSFORM_TEX(node_9941, _node_965),0.0,0));
                float3 node_7604 = clamp(node_324.rgb,0.0,0.9);
                float node_3701 = (o.uv0.g*sin(_node_3435));
                v.vertex.xyz += (lerp(node_7604,dot(node_7604,float3(0.3,0.59,0.11)),1.0)*_heightmultiply*v.normal*saturate((clamp(node_3701,-0.5,1)*10.0)));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform sampler2D _node_6603; uniform float4 _node_6603_ST;
            uniform sampler2D _node_6603_copy; uniform float4 _node_6603_copy_ST;
            uniform sampler2D _node_965; uniform float4 _node_965_ST;
            uniform float _heightmultiply;
            uniform float _node_3435;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_2245 = _Time + _TimeEditor;
                float2 node_2018 = (o.uv0+node_2245.r*float2(0.15,0.3));
                float4 _node_6603_var = tex2Dlod(_node_6603,float4(TRANSFORM_TEX(node_2018, _node_6603),0.0,0));
                float4 node_7058 = _Time + _TimeEditor;
                float2 node_1594 = (o.uv0+node_7058.r*float2(0.4,0.4));
                float4 _node_6603_copy_var = tex2Dlod(_node_6603_copy,float4(TRANSFORM_TEX(node_1594, _node_6603_copy),0.0,0));
                float2 node_9941 = float2(dot(((_node_6603_var.rgb*_node_6603_copy_var.rgb)*1.75+0.0),float3(0.3,0.59,0.11)),0.0);
                float4 node_324 = tex2Dlod(_node_965,float4(TRANSFORM_TEX(node_9941, _node_965),0.0,0));
                float3 node_7604 = clamp(node_324.rgb,0.0,0.9);
                float node_3701 = (o.uv0.g*sin(_node_3435));
                v.vertex.xyz += (lerp(node_7604,dot(node_7604,float3(0.3,0.59,0.11)),1.0)*_heightmultiply*v.normal*saturate((clamp(node_3701,-0.5,1)*10.0)));
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_2245 = _Time + _TimeEditor;
                float2 node_2018 = (i.uv0+node_2245.r*float2(0.15,0.3));
                float4 _node_6603_var = tex2D(_node_6603,TRANSFORM_TEX(node_2018, _node_6603));
                float4 node_7058 = _Time + _TimeEditor;
                float2 node_1594 = (i.uv0+node_7058.r*float2(0.4,0.4));
                float4 _node_6603_copy_var = tex2D(_node_6603_copy,TRANSFORM_TEX(node_1594, _node_6603_copy));
                float2 node_9941 = float2(dot(((_node_6603_var.rgb*_node_6603_copy_var.rgb)*1.75+0.0),float3(0.3,0.59,0.11)),0.0);
                float4 node_324 = tex2D(_node_965,TRANSFORM_TEX(node_9941, _node_965));
                float3 node_7604 = clamp(node_324.rgb,0.0,0.9);
                o.Emission = node_7604;
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
