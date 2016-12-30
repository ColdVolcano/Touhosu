// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Timing;

namespace osu.Framework.Graphics.Transformations
{
    public class TransformPosition : TransformVector
    {
        public override void Apply(Drawable d)
        {
            base.Apply(d);
            d.Position = CurrentValue;
        }
    }
}
