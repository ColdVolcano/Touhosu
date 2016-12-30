#ifdef GL_ES
	precision mediump float;
#endif

#include "sh_Utils.h"

varying vec2 v_DrawingPosition;
varying vec2 v_MaskingPosition;
varying vec4 v_Colour;
varying vec2 v_TexCoord;

uniform sampler2D m_Sampler;
uniform float g_CornerRadius;
uniform vec4 g_MaskingRect;
uniform float g_BorderThickness;
uniform vec4 g_BorderColour;

uniform float g_MaskingBlendRange;

uniform vec4 g_DrawingRect;
uniform vec2 g_DrawingBlendRange;

float distanceFromRoundedRect()
{
	// Compute offset distance from masking rect in masking space.
	vec2 topLeftOffset = g_MaskingRect.xy - v_MaskingPosition;
	vec2 bottomRightOffset = v_MaskingPosition - g_MaskingRect.zw;

	vec2 distanceFromShrunkRect = max(
		bottomRightOffset + vec2(g_CornerRadius),
		topLeftOffset + vec2(g_CornerRadius));

	float maxDist = max(distanceFromShrunkRect.x, distanceFromShrunkRect.y);

	// Inside the shrunk rectangle
	if (maxDist <= 0.0)
		return maxDist;
	// Outside of the shrunk rectangle
	else
		return length(max(vec2(0.0), distanceFromShrunkRect));
}

float distanceFromDrawingRect()
{
	vec2 topLeftOffset = g_DrawingRect.xy - v_DrawingPosition;
	vec2 bottomRightOffset = v_DrawingPosition - g_DrawingRect.zw;
	vec2 xyDistance = max(topLeftOffset, bottomRightOffset);
	return max(
		g_DrawingBlendRange.x > 0.0 ? xyDistance.x / g_DrawingBlendRange.x : 0.0,
		g_DrawingBlendRange.y > 0.0 ? xyDistance.y / g_DrawingBlendRange.y : 0.0);
}

void main(void)
{
	float dist = distanceFromRoundedRect() / g_MaskingBlendRange;

	// This correction is needed to avoid fading of the alpha value for radii below 1px.
	float radiusCorrection = g_CornerRadius <= 0.0 ? g_MaskingBlendRange : max(0.0, g_MaskingBlendRange - g_CornerRadius);
	float fadeStart = (g_CornerRadius + radiusCorrection) / g_MaskingBlendRange;
	float alphaFactor = min(fadeStart - dist, 1.0);

	if (g_DrawingBlendRange.x > 0.0 || g_DrawingBlendRange.y > 0.0)
		alphaFactor *= clamp(1.0 - distanceFromDrawingRect(), 0.0, 1.0);

	if (alphaFactor <= 0.0)
	{
		gl_FragColor = vec4(0.0);
		return;
	}

	// This ends up softening glow without negatively affecting edge smoothness much.
	alphaFactor *= alphaFactor;

	float borderStart = 1.0 + fadeStart - g_BorderThickness;
	float colourWeight = min(borderStart - dist, 1.0);
	if (colourWeight <= 0.0)
	{
		gl_FragColor = toSRGB(vec4(g_BorderColour.rgb, g_BorderColour.a * alphaFactor));
		return;
	}

	gl_FragColor = toSRGB(
		colourWeight * vec4(v_Colour.rgb, v_Colour.a * alphaFactor) * texture2D(m_Sampler, v_TexCoord, -0.9) +
		(1.0 - colourWeight) * g_BorderColour);
}
