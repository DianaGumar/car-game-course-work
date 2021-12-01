using RaceGame.Api.Common.GameObjects.Car;

namespace RaceGame.Api.Services.CarService
{
    public interface ICarService
    {
        public Car CreateCar(string clientId);
        public int AddCar(Car car);
        public Car GetCar(string clientId);
        public void UpdateCar(Car car);

    }
}
