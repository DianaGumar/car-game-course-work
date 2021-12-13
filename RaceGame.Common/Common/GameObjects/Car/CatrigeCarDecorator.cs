namespace RaceGame.Api.Common.GameObjects.Car
{
    public class CatrigeCarDecorator : CarDecorator
    {
        public CatrigeCarDecorator(Car car, int catridges) : base(car)
        {
            car.Cartridges = catridges;
            IsDecorate = false;
        }
    }
}
