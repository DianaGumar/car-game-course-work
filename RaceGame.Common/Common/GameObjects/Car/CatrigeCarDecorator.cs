namespace RaceGame.Api.Common.GameObjects.Car
{
    class CatrigeCarDecorator : CarDecorator
    {
        public CatrigeCarDecorator(Car car, int catridges) : base(car)
        {
            car.Cartridges = catridges;
            IsDecorate = false;
        }
    }
}
