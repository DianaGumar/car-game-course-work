using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using RaceGame.Api.Services.CarService;
using RaceGame.Api.Services.LevelService;
using RaceGame.Api.Services.MoveService;
using RaceGame.Api.Services.PrizeService;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RaceGame.Api.Services.GameService
{
    public class GameService : IGameService
    {
        private bool _isGameStarted;
        private Timer prizeTimer;
       
        private readonly ICarService _carService;
        private readonly IMoveService _moveService;
        private readonly IPrizeService _prizeService;
        private readonly ILevelService _levelService;

        public GameService(ICarService carService, IMoveService moveService, 
            IPrizeService prizeService, ILevelService levelService)
        {
            _carService = carService;
            _moveService = moveService;
            _prizeService = prizeService;
            _levelService = levelService;

            _isGameStarted = false;

            // таймер на генерацию призовых объектов - устанавливаем метод обратного вызова
            // создаем таймер - на каждые n сек
            prizeTimer = new Timer(new TimerCallback(RefreshPrizes), null, 0, 60000);
            RefreshPrizes(null);
        }

        public List<GameObject> GetAllObjects()
        {
            var gameObjects = new List<GameObject>(_prizeService.GetGamePrizes());
            gameObjects = gameObjects.Concat(_levelService.GetLevel()).ToList();
            gameObjects = gameObjects.Concat(_carService.GetCars()).ToList();

            return gameObjects;
        }

        // генерируем призам новые позиции и устанавливаем флаг - активен
        private void RefreshPrizes(object obj)
        {
            // обновляем координаты призов с учётом коллизий
            var gameobj = GetAllObjects().ToArray();
            _prizeService.RefreshPrizes(gameobj);
        }

        public Car AddGamer(string clientId)
        {
            // если игрок первый подключившийся - инициировать все игровые объекты?
            // создаёт и добавляет все игровые объекты в себя со спрайтами
            Car car = null;
            var count = _carService.GetCars().Count;

            if (count < 2)
            {
                car = _carService.CreateCar(clientId);
                var gamersCount = _carService.AddCar(car);
            }
            else if (count == 1)
            {
                StartGame();
            }
            else
            {
                car = _carService.GetCars().FirstOrDefault();
            }

            return car;
        }

        public void ResetGame()
        {
            RefreshPrizes(null);
            // ставим геймеров на начальные позиции устанавливая все значения по умолчанию.
        }

        private void StartGame()
        {
            // как только подсоединилось два и больше игрока.

            // инициализация начальных объектов

            // 
            
        }
    }
}
