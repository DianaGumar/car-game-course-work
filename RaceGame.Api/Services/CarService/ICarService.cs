using RaceGame.Api.Common.GameObjects.Car;
using System.Collections.Generic;

namespace RaceGame.Api.Services.CarService
{
    public interface ICarService
    {
        public Car CreateCar(string clientId);
        public int AddCar(Car car);
        public Car GetCar(string clientId);
        public Car GetEnemyCar(string clientId);
        public List<Car> GetCars();
        public void UpdateCar(Car car);

        public void DeleteCar(string id);

    }
}
