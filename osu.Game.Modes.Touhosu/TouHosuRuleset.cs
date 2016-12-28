//Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
//Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Modes.Objects;
using osu.Game.Modes.UI;
using osu.Game.Modes.Touhosu.Objects;
using System;

namespace osu.Game.Modes.TouHosu
{
    public class TouHosuRuleset : Ruleset
    {

        protected override PlayMode PlayMode => PlayMode.Touhosu;

        public override ScoreOverlay CreateScoreOverlay() => new TouhosuScoreOverlay();

        public override HitObjectParser CreateHitObjectParser() => new TouhosuObjectParser();

        public override ScoreProcessor CreateScoreProcessor() => new TouhosuScoreProcessor();

        public override HitRenderer CreateHitRendererWith(List<HitObject> objects);


    }
}
