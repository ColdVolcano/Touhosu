﻿// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using OpenTK.Graphics.ES30;

namespace osu.Framework.Graphics.OpenGL.Textures
{
    /// <summary>
    /// A TextureGL which is acting as the backing for an atlas.
    /// </summary>
    class TextureGLAtlas : TextureGLSingle
    {
        public TextureGLAtlas(int width, int height, bool manualMipmaps, All filteringMode = All.Linear)
            : base(width, height, manualMipmaps, filteringMode)
        {
        }
    }
}