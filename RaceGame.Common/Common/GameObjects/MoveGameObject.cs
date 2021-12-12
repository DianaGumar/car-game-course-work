namespace RaceGame.Api.Common.GameObjects
{
    public class MoveGameObject : GameObject
    {
        public float Speed { get; set; }
        public float MaxSpeed { get; set; }

        //public float DirectionX { get; set; } // the second dot of vector. First is position

        //public float DirectionY { get; set; }

        public float Fuel { get; set; }
        public float SpeedChange { get; set; } // when speed step

        public float OldPositionX { get; set; }
        public float OldPositionY { get; set; }
    }
}
