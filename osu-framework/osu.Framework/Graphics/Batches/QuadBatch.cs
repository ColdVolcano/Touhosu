﻿// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using osu.Framework.Graphics.OpenGL.Buffers;
using OpenTK.Graphics.ES30;

namespace osu.Framework.Graphics.Batches
{
    public class QuadBatch<T> : VertexBatch<T> where T : struct, IEquatable<T>
    {
        public QuadBatch(int size, int maxBuffers)
            : base(size, maxBuffers)
        {
        }

        protected override VertexBuffer<T> CreateVertexBuffer() => new QuadVertexBuffer<T>(Size, BufferUsageHint.DynamicDraw);
    }
}
