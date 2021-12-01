namespace RaceGame.Api.Common.GameObjects.Car
{
    public class Car : MoveGameObject
    {
        public int Fuel { get; set; }
        public bool IsFailingTire { get; set; }
        public int Cartridges { get; set; }
    }
}
