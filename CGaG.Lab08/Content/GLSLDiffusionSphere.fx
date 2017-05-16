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
uniform float VectorToLightLength;
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
		float3 normal = float3(dx, dy, -dz);
		float angle = abs(acos(dot(normal, VectorToLight) / (0.5f * VectorToLightLength)));
		float light = -2.0f / M_PI * angle + 1.0f;
		return float4(light, light, light, 1.0f) * LightColor;
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