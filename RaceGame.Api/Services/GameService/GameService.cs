using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using RaceGame.Api.Services.CarService;
using RaceGame.Api.Services.MoveService;
using System.Collections.Generic;

namespace RaceGame.Api.Services.GameService
{
    public class GameService : IGameService
    {
        private bool isGameStarted;
        //private int amountPrizes = 10;
        //private int timeDeltaSpawn = 10;
        //private int timeSpawn;
        //private Random random;

        private List<GameObject> gameObjects;
        

        private readonly ICarService _carService;
        private readonly IMoveService _moveService;

        public GameService(ICarService carService, IMoveService moveService)
        {
            _carService = carService;
            _moveService = moveService;

            isGameStarted = false;
            gameObjects = new List<GameObject>();
            //timeSpawn = DateTime.Now.Second;
            //random = new Random();

            // создаём карту
        }

        public void ResetGame()
        {
            gameObjects.Clear();
            // ставим геймеров на начальные позиции устанавливая все значения по умолчанию.

        }

        public Car AddGamer(string clientId)
        {
            var car = _carService.CreateCar(clientId);
            // поставить координаты на начало карты
            // задаём отображенчиские опции
            car.SizeX = 5;
            car.SizeY = 5;

            car.PositionX = 1;
            car.PositionY = 1;

            car.Speed = 1;

            var gamersCount = _carService.AddCar(car);
            if(gamersCount > 1)
            {
                StartGame();
            }

            return car;
        }

        public Car MoveGamer(string clientId, string gameObjectId, int direction)
        {
            // получаем необходимого игрока
            var car = _carService.GetCar(clientId, gameObjectId);
            
            switch (direction)
            {
                case 1: 
                { 
                    car = (Car)_moveService.MoveForward(car); 
                    break; 
                }
                case 2:
                {
                    car = (Car)_moveService.MoveBack(car);
                    break;
                }
                case 3:
                {
                    car = (Car)_moveService.MoveRight(car);
                    break;
                }
                case 4:
                {
                    car = (Car)_moveService.MoveLeft(car);
                    break;
                }
            }

            _carService.UpdateCar(car);
            return car;
        }

        private void StartGame()
        {
            // как только подсоединилось два и больше игрока.

            // инициализация начальных объектов

            // 
            isGameStarted = true;
        }
    }
}
