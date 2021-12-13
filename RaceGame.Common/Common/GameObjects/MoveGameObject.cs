namespace RaceGame.Api.Common.GameObjects
{
    public class MoveGameObject : GameObject
    {
        public virtual float Speed { get; set; }
        public virtual float MaxSpeed { get; set; }
        public virtual float Fuel { get; set; }
        public virtual float SpeedChange { get; set; } // when speed step
        public virtual float OldPositionX { get; set; }
        public virtual float OldPositionY { get; set; }
    }
}
