#define M_PI 3.1415926535897932384626433832795
#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

uniform float3 VectorToLight;
uniform float4 LightColor;

sampler2D SpriteTextureSampler = sampler_state {
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput {
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR {
	float dx = input.TextureCoordinates.x - 0.5f;
	float dy = input.TextureCoordinates.y - 0.5f;
	float dist = sqrt(dx * dx + dy * dy);
	if (dist < 0.5f) {
		float dz = sqrt(0.25f - dist * dist);
		float3 normal = normalize(float3(dx, dy, -dz));
		float3 light = normalize(VectorToLight - normal);
		float diffuse = max(dot(normal, light), 0.0f);
		float3 r = normalize(2.0f * dot(light, normal) * normal - light);
		float3 v = float3(0, 0, -1);
		float specular = max(pow(max(0, dot(r, v)), 32), 0);
		float intensiv = max(specular, diffuse);
		return float4(intensiv, intensiv, intensiv, 1.0f) * LightColor;
	}
	else {
		return float4(0, 0, 0, 0);
	}
	//return float4(input.TextureCoordinates.x, input.TextureCoordinates.y, 1, 1);
	//return tex2D(SpriteTextureSampler, input.TextureCoordinates) * input.Color;
}

technique SpriteDrawing {
	pass P0 {
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};