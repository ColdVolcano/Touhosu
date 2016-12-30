﻿// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

namespace osu.Framework.Timing
{
    public struct FrameTimeInfo
    {
        /// <summary>
        /// Elapsed time during last frame in milliseconds.
        /// </summary>
        public double Elapsed;

        /// <summary>
        /// Begin time of this frame.
        /// </summary>
        public double Current;
    }
}