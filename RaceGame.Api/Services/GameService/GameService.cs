using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using RaceGame.Api.Services.CarService;
using RaceGame.Api.Services.MoveService;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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
            // если игрок первый подключившийся - инициировать все игровые объекты?
            // создаёт и добавляет все игровые объекты в себя со спрайтами
            Car car = null;
            var count = _carService.GetCars().Count;

            if (count < 2)
            {
                car = _carService.CreateCar(clientId);
                var gamersCount = _carService.AddCar(car);
            }
            else if(count == 1)
            {
                StartGame();
            }
            else
            {
                car = _carService.GetCars().FirstOrDefault();
            }

            return car;
        }

        public Car MoveGamer(string clientId, int direction)
        {
            // получаем необходимого игрока
            var car = _carService.GetCar(clientId);
            
            switch (direction)
            {
                case 0:
                {
                    car = (Car)_moveService.UpdatePosition(car);
                    break;
                }
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
                    car = (Car)_moveService.RotateRight(car);
                    break;
                }
                case 4:
                {
                    car = (Car)_moveService.RotateLeft(car);
                    break;
                }
            }

            // проверка на коллизию
            if (!CheckCollision(car))
            {
                _carService.UpdateCar(car);

                return car;
            }
            else
            {
                return (Car)_moveService.ReturnPreviosState(_carService.GetCar(clientId));
            }
        }

        private void StartGame()
        {
            // как только подсоединилось два и больше игрока.

            // инициализация начальных объектов

            // 
            isGameStarted = true;
        }

        public void DeleteGamer(string clientId)
        {
            _carService.DeleteCar(clientId);
        }

        public List<GameObject> GetGameObjects(string gamerId)
        {
            // получает всех игроков кроме себя
            var result = gameObjects;
            result.AddRange(_carService.GetCars().Where(c => !c.Id.Equals(gamerId)).ToList());

            return result;
        }

        public List<GameObject> GetGameObjects()
        {
            return gameObjects;
        }

        public List<Car> GetAllGamers()
        {
            return _carService.GetCars();
        }

        // проверка на коллизию со свеми объектами
        private bool CheckCollision(GameObject gameObject)
        {
            var collision = false;

            var enemy = _carService.GetEnemyCar(gameObject.Id);
            if (enemy != null)
            {
                collision = collision || IsCollision(gameObject, enemy);
            }           

            foreach(var obj in gameObjects)
            {
                collision = collision || IsCollision(gameObject, obj);
            }

            return collision;
        }

        // алгоритм коллизии
        private bool IsCollision(GameObject r1, GameObject r2)
        {
            // прямоугольник х примоугольник
            float sizeSpecific = 0.89f;

            Vector2 pr1 = new Vector2(r1.PositionX, r1.PositionY);
            Vector2 pr2 = new Vector2(r2.PositionX, r2.PositionY);
            Vector2 sr1 = new Vector2(r1.SizeX, r1.SizeY);
            Vector2 sr2 = new Vector2(r2.SizeX, r2.SizeY);

            if (pr1.X + sr1.X * sizeSpecific >= pr2.X &&    // r1 right edge past r2 left
                pr1.X <= pr2.X + sr2.X * sizeSpecific &&    // r1 left edge past r2 right
                pr1.Y + sr1.Y * sizeSpecific >= pr2.Y &&    // r1 top edge past r2 bottom
                pr1.Y <= pr2.Y + sr2.Y * sizeSpecific)      // r1 bottom edge past r2 top
            {
                return true;
            }
            return false;
        }

        //private Vector2 RandomPosition(Vector2 min, Vector2 max)
        //{
        //    Vector2 cord = new Vector2();

        //    cord.X = (float)(rand.NextDouble() * (max.X - min.X) + min.X);
        //    cord.Y = (float)(rand.NextDouble() * (max.Y - min.Y) + min.Y);

        //    return cord;
        //}
    }
}
