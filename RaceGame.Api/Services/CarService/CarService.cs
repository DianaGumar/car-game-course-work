using RaceGame.Api.Common.GameObjects.Car;
using System.Collections.Generic;
using System.Linq;

namespace RaceGame.Api.Services.CarService
{
    public class CarService : ICarService
    {
        private int maxFuel;
        private int startCartridges;

        private float maxSpeed;

        private readonly List<Car> gamers;

        public CarService()
        {
            gamers = new List<Car>();

            maxSpeed = 100;
            maxFuel = 100;
            startCartridges = 3;
        }

        public Car CreateCar(string clientId)
        {
            var car = new Car()
            {
                Id = clientId,
                PositionX = 150,
                PositionY = 190,
                SizeX = 61,
                SizeY = 24,
                Speed = 0,
                MaxSpeed = maxSpeed,
                SpeedChange = 1,
                Angle = 0,
                Fuel = maxFuel,
                Cartridges = startCartridges,
                IsFailingTire = false
            };

            return car;
        }

        public Car GetCar(string clientId)
        {
            return gamers.FirstOrDefault(c => c.Id.Equals(clientId));
        }

        public int AddCar(Car car)
        {
            gamers.Add(car);

            return gamers.Count();
        }

        public void UpdateCar(Car car)
        {
            var originCar = gamers.FirstOrDefault(c => c.Id.Equals(car.Id));
            gamers.Remove(originCar);
            gamers.Add(car);
        }
    }
}
