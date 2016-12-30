﻿// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using osu.Framework.Desktop;
using osu.Framework.Platform;
using osu.Framework;

namespace SampleGame
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (BaseGame game = new SampleGame())
            using (BasicGameHost host = Host.GetSuitableHost(@"sample-game"))
            {
                host.Add(game);
                host.Run();
            }
        }
    }
}
