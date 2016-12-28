using System;
using osu.Game.Graphics.UserInterface;
using osu.Game.Modes.UI;

namespace osu.Game.Modes.TouHosu
{
    internal class TouhosuScoreOverlay : ScoreOverlay
    {
        protected override PercentageCounter CreateAccuracyCounter()
        {
            throw new NotImplementedException();
        }

        protected override ComboCounter CreateComboCounter()
        {
            throw new NotImplementedException();
        }

        protected override KeyCounterCollection CreateKeyCounter()
        {
            throw new NotImplementedException();
        }

        protected override ScoreCounter CreateScoreCounter()
        {
            throw new NotImplementedException();
        }
    }
}