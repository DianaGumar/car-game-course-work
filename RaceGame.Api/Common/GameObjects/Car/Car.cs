namespace RaceGame.Api.Common.GameObjects.Car
{
    public class Car : MoveGameObject
    {
        public string ClientIp { get; set; }
        public int Fuel { get; set; }
        public bool IsFailingTire { get; set; }
        public int Cartridges { get; set; }
    }
}
