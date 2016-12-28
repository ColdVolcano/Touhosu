using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Modes.Touhosu.Objects.Drawables;
using OpenTK;

namespace osu.Game.Modes.Touhosu.Objects
{
    class Player
    {
        //Basic structure all players should follow
        struct PlayerMetadata
        {
            public int Speed;
            public int playerNumber;
            public int playerTeam;
            public int Health;
            public int Damage;
            public string Name;
            public int Shot1Type;
            public int Shot2Type;
        }


    }
}
