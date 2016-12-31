using System;
using osu.Framework.Input;
using osu.Framework.Graphics.Transformations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Modes.Touhosu.Objects.Drawables
{
    //TODO Make this work and make it modular so I can put *MANY* on the screen at once
    class DrawablePlayer : Container
    {
        private Sprite reimu;

        const float size = 500;
        private Sprite player;

        public DrawablePlayer(Player player)
        {

            //Position = player.Position;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            AutoSizeAxes = Axes.Both;
            Alpha = 1;
            Masking = true;

            Children = new Drawable[]
            {
                reimu = new Sprite
                {
                    Size = new Vector2(size,size),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                }
            };
        }

        //TODO Fix this
        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            reimu.Texture = textures.Get(@"play/Touhosu/Reimu");
        }
    }
}
