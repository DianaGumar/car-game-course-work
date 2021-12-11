using OpenTK.Mathematics;
using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using RaceGame.Api.Services.CarService;
using RaceGame.Api.Services.MoveService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
//using System.Numerics;

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
        private GameObject[] _levelObjects;
        private string[] _levelsRightSequence;

        private readonly ICarService _carService;
        private readonly IMoveService _moveService;

        public GameService(ICarService carService, IMoveService moveService)
        {
            _carService = carService;
            _moveService = moveService;

            isGameStarted = false;
            gameObjects = new List<GameObject>();

            // создаём карту
            Vector2[] positions =
            {
                new Vector2(0,0),
                new Vector2(0,100),
                new Vector2(0,200),
                new Vector2(0,300),
                new Vector2(0,400),
                new Vector2(0,500),
                new Vector2(0,600),
                new Vector2(100,0),
                new Vector2(200,0),
                new Vector2(300,0),
                new Vector2(400,0),
                new Vector2(600,0),
                new Vector2(600,0),
                new Vector2(700,0),
                new Vector2(800,0),
                new Vector2(900,0),
                new Vector2(1000,0),
                new Vector2(1100,0),
                new Vector2(1100,100),
                new Vector2(1100,200),
                new Vector2(1100,300),
                new Vector2(1100,400),
                new Vector2(1100,500),
                new Vector2(1100,600),
                new Vector2(100,600),
                new Vector2(200,600),
                new Vector2(300,600),
                new Vector2(400,600),
                new Vector2(600,600),
                new Vector2(600,600),
                new Vector2(700,600),
                new Vector2(800,600),
                new Vector2(900,600),
                new Vector2(1000,600),
                new Vector2(1100,600),

                new Vector2(100,100),
                new Vector2(200,100),
                new Vector2(600,100),

                new Vector2(400,200),
                new Vector2(800,200),
                new Vector2(900,200),

                new Vector2(200,300),
                new Vector2(300,300),
                new Vector2(400,300),
                new Vector2(500,300),
                new Vector2(600,300),
                new Vector2(700,300),
                new Vector2(800,300),
                new Vector2(900,300),

                new Vector2(200,400),
                new Vector2(300,400),
                new Vector2(400,400),
                new Vector2(500,400),
                new Vector2(600,400),
                new Vector2(700,400),
                new Vector2(800,400),
                new Vector2(900,400),

                

                //new Vector2(848,626),
                //new Vector2(586,466),
                //new Vector2(297,626),
                //new Vector2(143,319),
            };
            //Vector2[] sizes =
            //{
            //    //new Vector2(100,100),
            //    //new Vector2(100,100),
            //    //new Vector2(100,100),
            //    //new Vector2(100,100),
            //    new Vector2(100,100),

            //    //new Vector2(286,122),
            //    //new Vector2(392,160),
            //    //new Vector2(430,122),
            //    //new Vector2(154,429),
            //};

            _levelObjects = new GameObject[positions.Count()];
            _levelsRightSequence = new string[positions.Count()];
            for (int i = 0; i < positions.Count(); i++)
            {
                _levelObjects[i] = new GameObject()
                {
                    Id = i.ToString(), //Guid.NewGuid().ToString(),
                    PositionX = positions[i].X,
                    PositionY = positions[i].Y,
                    SizeX = 100, //sizes[i].X,
                    SizeY = 100 //sizes[i].Y
                };

                _levelsRightSequence[i] = _levelObjects[i].Id;
            }
        }

        public GameObject[] GetLevel()
        {
            return _levelObjects;
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
            var isCollizion = false;

            if (car.Speed != 0)
            {
                isCollizion = CheckCollision(car);
            }

            if (!isCollizion)
            {
                car.IsCollizion = isCollizion;
                _carService.UpdateCar(car);         
                return car;
            }
            else
            {
                var newcar = (Car)_moveService.ReturnPreviosState(_carService.GetCar(clientId));
                newcar.IsCollizion = isCollizion;
                return newcar;
            }
        }

        private void StartGame()
        {
            // как только подсоединилось два и больше игрока.

            // инициализация начальных объектов

            // 
            
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

        private Car CreckLevelsSequense(Car gameObject, string levelId)
        {
            //gameObject.RightLevelsSequence[]

            return gameObject;
        }

        // проверка на коллизию со свеми объектами
        private bool CheckCollision(Car gameObject)
        {
            var collision = false;

            foreach (var obj in _levelObjects)
            {
                // если игрок находится в коллизии с одной из частей уровня
                collision = collision || CollisionHelper.AABBAndAABBB(
                    new Vector2(gameObject.PositionX, gameObject.PositionY),
                    new Vector2(gameObject.SizeX, gameObject.SizeY),
                    new Vector2(obj.PositionX, obj.PositionY),
                    new Vector2(obj.SizeX, obj.SizeY));
                if (collision)
                {
                    // логика по прохождению кругов игроками
                    //CreckLevelsSequense(gameObject, obj.Id);

                    gameObject.Speed = 0.1f;

                    break;
                }
            }

            var enemy = _carService.GetEnemyCar(gameObject.Id);
            if (enemy != null)
            {
                collision = collision || CollisionHelper.AABBAndAABBB(
                    new Vector2(gameObject.PositionX, gameObject.PositionY),
                    new Vector2(gameObject.SizeX, gameObject.SizeY),
                    new Vector2(enemy.PositionX, enemy.PositionY),
                    new Vector2(enemy.SizeX, enemy.SizeY));
            }

            //foreach(var obj in gameObjects)
            //{
            //    collision = collision || IsCollissionRectangle(gameObject, obj);
            //}

            return collision;
        }

        //// коллизия габаритов
        //private bool MacroCollision(GameObject obj1, GameObject obj2)
        //{
        //    var XColl = false;
        //    var YColl = false;

        //    if ((obj1.PositionX + obj1.SizeX >= obj2.PositionX) && 
        //        (obj1.PositionX <= obj2.PositionX + obj2.SizeX)) XColl = true;
        //    if ((obj1.PositionY + obj1.SizeY >= obj2.PositionY) && 
        //        (obj1.PositionY <= obj2.PositionY + obj2.SizeY)) YColl = true;

        //    if (XColl & YColl) { return true; }
        //    return false;
        //}

        //// алгоритм коллизии по .net прямоугольникам
        //private bool IsCollissionRectangle(GameObject r1, GameObject r2)
        //{
        //    var rr1 = new Rectangle((int)(r1.PositionX*100), (int)(r1.PositionY*100), 
        //        (int)(r1.SizeX*100), (int)(r1.SizeY*100));
        //    var rr2 = new Rectangle((int)(r2.PositionX*100), (int)(r2.PositionY*100), 
        //        (int)(r2.SizeX*100), (int)(r2.SizeY*100));

        //    return rr1.IntersectsWith(rr2);
        //}

        //// коллизия прямоугольник х примоугольник без учёта угла заворота
        //private bool IsCollision(GameObject r1, GameObject r2)
        //{
        //    float sizeSpecific = 0.89f;

        //    Vector2 pr1 = new Vector2(r1.PositionX, r1.PositionY);
        //    Vector2 pr2 = new Vector2(r2.PositionX, r2.PositionY);
        //    Vector2 sr1 = new Vector2(r1.SizeX, r1.SizeY);
        //    Vector2 sr2 = new Vector2(r2.SizeX, r2.SizeY);

        //    if (pr1.X + sr1.X * sizeSpecific >= pr2.X &&    // r1 right edge past r2 left
        //        pr1.X <= pr2.X + sr2.X * sizeSpecific &&    // r1 left edge past r2 right
        //        pr1.Y + sr1.Y * sizeSpecific >= pr2.Y &&    // r1 top edge past r2 bottom
        //        pr1.Y <= pr2.Y + sr2.Y * sizeSpecific)      // r1 bottom edge past r2 top
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private Vector2 RandomPosition(Vector2 min, Vector2 max)
        //{
        //    Vector2 cord = new Vector2();

        //    cord.X = (float)(rand.NextDouble() * (max.X - min.X) + min.X);
        //    cord.Y = (float)(rand.NextDouble() * (max.Y - min.Y) + min.Y);

        //    return cord;
        //}
    }
}
