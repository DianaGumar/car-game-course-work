using RaceGame.Api.Common.GameObjects;
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
        public void UpdateCarTexture(Car car);
        public void DeleteCar(string id);
        public Car MoveGamer(string clientId, int direction);
        public void ResetCars();
        public Bullet[] GetBullets();
        public Bullet GetShot(string carId);
    }
}
