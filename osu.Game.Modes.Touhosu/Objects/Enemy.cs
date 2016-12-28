using OpenTK;

namespace osu.Game.Modes.Touhosu.Objects
{
    public class Enemy
    {
        public bool NewCombo { get; set; }
        public Vector2 Position { get; set; }
        public int StartTime { get; set; }

        struct EnemyMetadata
        {
            private int Number;
            private int Team;
            private int Health;
            private int Damage;
            private int ShotType;
            private int EnemyType;
        }
    }
}