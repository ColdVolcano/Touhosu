﻿// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Textures;
using OpenTK.Graphics.ES30;
using osu.Framework.Graphics.OpenGL;
using OpenTK;
using System;

namespace osu.Framework.Graphics.Sprites
{
    public class SpriteDrawNode : DrawNode
    {
        public Texture Texture;
        public Quad ScreenSpaceDrawQuad;
        public RectangleF DrawRectangle;
        public Vector2 InflationAmount;
        public bool WrapTexture;

        public Shader TextureShader;
        public Shader RoundedTextureShader;

        private bool NeedsRoundedShader => GLWrapper.IsMaskingActive || InflationAmount != Vector2.Zero;

        protected virtual void Blit(Action<TexturedVertex2D> vertexAction)
        {
            Texture.DrawQuad(ScreenSpaceDrawQuad, DrawInfo.Colour, null, vertexAction,
                new Vector2(InflationAmount.X / DrawRectangle.Width, InflationAmount.Y / DrawRectangle.Height));
        }

        public override void Draw(Action<TexturedVertex2D> vertexAction)
        {
            base.Draw(vertexAction);

            if (Texture == null || Texture.IsDisposed)
                return;

            Shader shader = NeedsRoundedShader ? RoundedTextureShader : TextureShader;

            if (InflationAmount != Vector2.Zero)
            {
                // The shader currently cannot deal with negative width and height.
                RectangleF drawRect = DrawRectangle.WithPositiveExtent;
                RoundedTextureShader.GetUniform<Vector4>(@"g_DrawingRect").Value = new Vector4(
                    drawRect.Left,
                    drawRect.Top,
                    drawRect.Right,
                    drawRect.Bottom);

                RoundedTextureShader.GetUniform<Matrix3>(@"g_ToDrawingSpace").Value = DrawInfo.MatrixInverse;
                RoundedTextureShader.GetUniform<Vector2>(@"g_DrawingBlendRange").Value = InflationAmount;
            }

            shader.Bind();

            Texture.TextureGL.WrapMode = WrapTexture ? TextureWrapMode.Repeat : TextureWrapMode.ClampToEdge;

            Blit(vertexAction);

            shader.Unbind();

            if (InflationAmount != Vector2.Zero)
                RoundedTextureShader.GetUniform<Vector2>(@"g_DrawingBlendRange").Value = Vector2.Zero;
        }
    }
}
