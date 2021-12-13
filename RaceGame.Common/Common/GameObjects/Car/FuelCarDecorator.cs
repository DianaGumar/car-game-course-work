namespace RaceGame.Api.Common.GameObjects.Car
{
    public class FuelCarDecorator : CarDecorator
    {
        public FuelCarDecorator(Car car, float fuel) : base(car)
        {
            car.Fuel = fuel;
            IsDecorate = false;
        }
    }
}
