﻿// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using System.Collections.Generic;
using System.Drawing;
using osu.Framework.Graphics.OpenGL.Textures;
using OpenTK.Graphics.ES30;
using osu.Framework.Graphics.Sprites;

namespace osu.Framework.Graphics.Textures
{
    public class TextureAtlas
    {
        // We are adding an extra padding on top of the padding required by
        // mipmap blending in order to support smooth edges without antialiasing which requires
        // inflating texture rectangles.
        private const int PADDING = (1 << TextureGLSingle.MAX_MIPMAP_LEVELS) + Sprite.MAX_EDGE_SMOOTHNESS * 2;

        private List<Rectangle> subTextureBounds = new List<Rectangle>();
        private TextureGLSingle atlasTexture;

        private int atlasWidth;
        private int atlasHeight;

        private int currentY;

        private int mipmapLevels => (int)Math.Log(atlasWidth, 2);

        private bool manualMipmaps;
        private All filteringMode;

        public TextureAtlas(int width, int height, bool manualMipmaps = false, All filteringMode = All.Linear)
        {
            atlasWidth = width;
            atlasHeight = height;
            this.manualMipmaps = manualMipmaps;
            this.filteringMode = filteringMode;
        }

        public void Reset()
        {
            subTextureBounds.Clear();
            currentY = 0;

            //may be zero in a headless context.
            if (atlasWidth == 0 || atlasHeight == 0)
                return;

            atlasTexture = new TextureGLAtlas(atlasWidth, atlasHeight, manualMipmaps, filteringMode);

            using (var whiteTex = Add(3, 3))
            {
                //add an empty white rect to use for solid box drawing (shader optimisation).
                byte[] white = new byte[whiteTex.Width * whiteTex.Height * 4];
                for (int i = 0; i < white.Length; i++)
                    white[i] = 255;
                whiteTex.SetData(new TextureUpload(white));
            }
        }

        private Point findPosition(int width, int height)
        {
            if (atlasHeight == 0 || atlasWidth == 0) return Point.Empty;

            if (currentY + height > atlasHeight)
                Reset();

            // Super naive implementation only going from left to right.
            Point res = new Point(0, currentY);

            int maxY = currentY;
            foreach (Rectangle bounds in subTextureBounds)
            {
                // +1 is required to prevent aliasing issues with sub-pixel positions while drawing. Bordering edged of other textures can show without it.
                res.X = Math.Max(res.X, bounds.Right + PADDING);
                maxY = Math.Max(maxY, bounds.Bottom);
            }

            if (res.X + width > atlasWidth)
            {
                // +1 is required to prevent aliasing issues with sub-pixel positions while drawing. Bordering edged of other textures can show without it.
                currentY = maxY + PADDING;
                subTextureBounds.Clear();
                res = findPosition(width, height);
            }

            return res;
        }

        internal Texture Add(int width, int height)
        {
            lock (this)
            {
                if (atlasTexture == null)
                    Reset();

                Point position = findPosition(width, height);
                Rectangle bounds = new Rectangle(position.X, position.Y, width, height);
                subTextureBounds.Add(bounds);

                return new Texture(new TextureGLSub(bounds, atlasTexture));
            }
        }

        internal Texture GetWhitePixel()
        {
            if (atlasTexture == null)
                Reset();

            return new TextureWhitePixel(new TextureGLAtlasWhite(atlasTexture));
        }
    }
}
