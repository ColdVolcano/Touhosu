//Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
//Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Modes.Touhosu.Objects;
using osu.Game.Modes.Objects;
using osu.Game.Modes.Objects.Drawables;
using osu.Game.Modes.UI;
using osu.Framework.Graphics;
using OpenTK;

namespace osu.Game.Modes.Touhosu.UI
{
    public class TouhosuObjectRenderer
    {
        public List<Enemy> Enemy { get; internal set; }

        public List<Bullet> Bullet { get; internal set; }

        public List<Player> Player { get; internal set; }

        public List<HitObject> Objects { get; internal set; }
        public Anchor Anchor { get; set; }
        public Vector2 Scale { get; set; }
        public Anchor Origin { get; set; }

        protected Playfield CreatePlayfield() => new DodgePlayfield();

        protected DrawableHitObject GetVisualRepresentation(Enemy e) => null;// new DrawableEnemy(e);

        protected DrawableHitObject GetVisualRepresentation(Player p) => null;// new DrawablePlayer(p);
    }
}
