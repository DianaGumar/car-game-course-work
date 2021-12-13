namespace RaceGame.Api.Common.GameObjects.Car
{
    class TireCarDecorator : CarDecorator
    {
        public TireCarDecorator(Car car, bool tire) : base(car)
        {
            car.Tire = tire;
            IsDecorate = false;
        }
    }
}
