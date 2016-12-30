﻿// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.GameModes.Testing;
using osu.Framework.Graphics.Sprites;

namespace osu.Framework.VisualTests.Tests
{
    class TestCaseNestedHover : TestCase
    {
        public override string Name => @"Nested Hover";
        public override string Description => @"Hovering multiple nested elements";

        public override void Reset()
        {
            base.Reset();

            HoverBox box1;
            Add(box1 = new HoverBox(Color4.Gray, Color4.White)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(300, 300)
            });

            HoverBox box2;
            box1.Add(box2 = new HoverBox(Color4.Pink, Color4.Red)
            {
                RelativePositionAxes = Axes.Both,
                RelativeSizeAxes = Axes.Both,
                Position = new Vector2(0.2f, 0.2f),
                Size = new Vector2(0.6f, 0.6f)
            });

            box2.Add(new HoverBox(Color4.LightBlue, Color4.Blue, false)
            {
                RelativePositionAxes = Axes.Both,
                RelativeSizeAxes = Axes.Both,
                Position = new Vector2(0.2f, 0.2f),
                Size = new Vector2(0.6f, 0.6f)
            });
        }

        class HoverBox : Container
        {
            private Color4 normalColour;
            private Color4 hoveredColour;

            private Box box;
            private bool propagateHover;

            public HoverBox(Color4 normalColour, Color4 hoveredColour, bool propagateHover = true)
            {
                this.normalColour = normalColour;
                this.hoveredColour = hoveredColour;
                this.propagateHover = propagateHover;

                Children = new Drawable[]
                {
                    box = new Box()
                    {
                        Colour = normalColour,
                        RelativeSizeAxes = Axes.Both
                    }
                };
            }

            protected override bool OnHover(InputState state)
            {
                box.Colour = hoveredColour;
                return !propagateHover;
            }

            protected override void OnHoverLost(InputState state)
            {
                box.Colour = normalColour;
            }
        }
    }
}
