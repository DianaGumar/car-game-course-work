using RaceGame.Api.Common.GameObjects.Car;
using System;
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

            maxSpeed = 5;
            maxFuel = 100;
            startCartridges = 3;
        }

        public Car CreateCar(string clientIp)
        {
            var car = new Car()
            {
                Id = Guid.NewGuid().ToString(),
                ClientId = clientIp,
                Speed = 0,
                Fuel = maxFuel,
                Cartridges = startCartridges,
                IsFailingTire = false
            };

            return car;
        }

        public Car GetCar(string clientId, string carId)
        {
            return gamers.FirstOrDefault(c => c.Id.Equals(carId));
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
