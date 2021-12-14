using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using RaceGame.Api.Services.LevelService;
using RaceGame.Api.Services.MoveService;
using RaceGame.Api.Services.PrizeService;
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

        private List<Car> gamers;
        private List<Bullet> _shorts;

        private readonly IMoveService _moveService;
        private readonly IPrizeService _prizeService;
        private readonly ILevelService _levelService;

        public CarService(IMoveService moveService, 
            IPrizeService prizeService, ILevelService levelService)
        {
            _moveService = moveService;
            _prizeService = prizeService;
            _levelService = levelService;

            gamers = new List<Car>();
            _shorts = new List<Bullet>();

            maxSpeed = 100;
            maxFuel = 300;
            startCartridges = 10;
        }

        public Bullet[] GetBullets()
        {
            // обновляем состояние всех пуль (движение по заданной траектории)
            BulletsStep();

            return _shorts.ToArray();
        }

        public Bullet GetShot(string carId)
        {
            var gamer = GetCar(carId);

            var shot = new Bullet();
            shot.Id = Guid.NewGuid().ToString();
            shot.SizeX = 10;
            shot.SizeY = 10;
            shot.Speed = 0.9f;
            shot.SpeedChange = gamer.SpeedChange * 2;
            shot.PositionX = gamer.PositionX;
            shot.PositionY = gamer.PositionY;
            shot.Angle = gamer.Angle;
            shot.OwnerId = carId;

            var shotOld = _shorts.FirstOrDefault(s => s.IsDeactivate);
            _shorts.Remove(shotOld);

            _shorts.Add(shot);

            return shot;
        }

        public void ResetCars()
        {
            // пересоздаём
            try
            {
                gamers[0] = CreateCar(gamers[0].Id);
                gamers[1] = CreateCar(gamers[1].Id);
            }
            catch
            {
                // из-за неоднотактовой операции
            }
        }

        public Car CreateCar(string clientId)
        {
            float positionX = 110;
            float positionY = 210;

            float sizeX = 36;
            float sizeY = 16;

            if(gamers.Count> 0)
            {
                positionY = gamers[0].PositionY + sizeY + 10;
            }

            var car = new Car()
            {
                Id = clientId,
                PositionX = positionX,
                PositionY = positionY,
                SizeX = sizeX,
                SizeY = sizeY,
                Speed = 0,
                Tire = true,
                MaxSpeed = maxSpeed,
                MaxFuel = maxFuel,
                SpeedChange = 2,
                Angle = 0,
                Fuel = maxFuel/2,
                Cartridges = startCartridges,
                MaxCartridges = startCartridges,
                IsFailingTire = false,
            };

            var rs = _levelService.GetLevelRightSequensce();
            car.LevelsSequence = new bool[rs.Length];

            return car;
        }

        public Car GetEnemyCar(string clientId)
        {
            return gamers.FirstOrDefault(c => !c.Id.Equals(clientId));
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
            if (originCar != null) 
            {
                originCar = car;
            } 
        }

        public void UpdateCarTexture(Car car)
        {
            var originCar = gamers.FirstOrDefault(c => c.Id.Equals(car.Id));
            if (originCar != null)
            {
                originCar.SpriteId = car.SpriteId;
                originCar.SpriteSizeX = car.SpriteSizeX;
                originCar.SpriteSizeY = car.SpriteSizeY;
            }
        }

        public List<Car> GetCars()
        {
            return gamers;
        }

        public void DeleteCar(string id)
        {
            var removedGamer = gamers.First(g => g.Id.Equals(id));
            gamers.Remove(removedGamer);
        }

        public Car MoveGamer(string clientId, int direction)
        {
            // получаем необходимого игрока
            var car = GetCar(clientId);

            // обновляем параметры игрока
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

            // проверка на коллизию новых параметров игрока
            car = CheckAndUpdateWithLevelRigntSequenceCollision(car, _levelService.GetLevelRightSequensce());
            car = CheckAndUpdateWithPrizeCollision(car, _prizeService.GetGamePrizes());
            car = CheckAndUpdateWithBulletCollision(car, _shorts.ToArray());

            var isLevelCollision = CheckAndUpdateWithLevelCollision(ref car, _levelService.GetLevel());
            if (isLevelCollision)
            {
                UpdateCar(car);
                return car;
            }

            var enemy = GetEnemyCar(car.Id);
            var isEnemyCollision = CheckAndUpdateWithEnemyCollision(ref car, enemy);
            if (isLevelCollision)
            {
                UpdateCar(car);
                return car;
            }

            UpdateCar(car);

            return car;
        }

        private void BulletsStep()
        {
            for (int i = 0; i < _shorts.Count; i++)
            {
                if (!_shorts[i].IsDeactivate)
                {
                    string collisionObjId = null;
                    // проверка на коллизию со стенами
                    var isCollizion = CollisionHelper.CheckCollision(_shorts[i],
                        out collisionObjId, _levelService.GetLevel());

                    if (isCollizion)
                    {
                        _shorts[i].IsDeactivate = true;
                    }
                    else
                    {
                        _shorts[i] = (Bullet)_moveService.UpdatePosition(_shorts[i]);
                    }
                }
            }
        }

        private bool CheckAndUpdateWithEnemyCollision(ref Car car, Car enemy)
        {
            string collisionObjId = null;

            // проверка на коллизию с другим игроком
            var isCollizion = CollisionHelper.CheckCollision(car, out collisionObjId, enemy);
            if (isCollizion)
            {
                // возвращаем предыдущие значения координат
                car = (Car)_moveService.ReturnPreviosState(car);
            }

            car.IsCollizion = isCollizion;
            return isCollizion;
        }

        private Car CheckAndUpdateWithBulletCollision(Car car, Bullet[] bulllets)
        {
            string collisionObjId = null;

            // проверка на коллизию с другим игроком
            var isCollizion = CollisionHelper.CheckCollision(car, out collisionObjId, bulllets);
            
            if (isCollizion )
            {
                // находим нужную пулю
                var bullet = _shorts.FirstOrDefault(s => s.Id.Equals(collisionObjId));

                if (bullet != null && !bullet.OwnerId.Equals(car.Id) && !bullet.IsDeactivate)
                {
                    car.IsCollizion = isCollizion;
                    car.Tire = false;
                    car.Speed = car.Speed < 0.4f ? car.Speed : 0.4f;

                    bullet.IsDeactivate = true;
                }
            }
            
            return car;
        }

        private Car CheckAndUpdateWithLevelRigntSequenceCollision(Car car, GameObject[] levelRight)
        {
            string collisionObjId = null;

            // проверка на коллизию с отметкой уровня
            var isCollizion = CollisionHelper.CheckCollision(car, out collisionObjId, levelRight);
            if (isCollizion)
            {
                var id = int.Parse(collisionObjId);

                if(id == 1)
                {
                    car.LevelsSequence[4] = false;
                }

                if (car.LevelsSequence.Where(l => l == false).Count() == 0)
                {
                    // защитываем круг пройденным
                    car.RightLevelsSequence += 1;

                    // обнуляем значения
                    car.LevelsSequence = new bool[levelRight.Length];
                }
                else
                {
                    car.LevelsSequence[id] = true;
                }
            }

            return car;
        }

        private bool CheckAndUpdateWithLevelCollision(ref Car car, GameObject[] levels)
        {
            string collisionObjId = null;

            // проверка на коллизию с уровнем
            var isCollizion = CollisionHelper.CheckCollision(car, out collisionObjId, levels);
            if (isCollizion)
            {
                // замедление скорости
                car.Speed = 0.1f;
                // возвращаем предыдущие значения координат
                car = (Car)_moveService.ReturnPreviosState(car);
            }

            car.IsCollizion = isCollizion;
            return isCollizion;
        }

        private Car CheckAndUpdateWithPrizeCollision(Car car, GameObject[] prizes)
        {
            string collisionObjId = null;

            // проверка на коллизию с призом
            var isPrizeCollizion = CollisionHelper.CheckCollision(car, out collisionObjId, prizes);
            if (isPrizeCollizion)
            {
                if (car.PrizeId == null || !car.PrizeId.Equals(collisionObjId))
                {
                    // логика набрасывания приза на игрока
                    car.PrizeId = collisionObjId;

                    var prize = _prizeService.GetGamePrize(collisionObjId);
                    if (prize != null && !prize.IsDeactivate)
                    {
                        // декорируем машину
                        car = Decorate(car, prize.Name);
                    }

                    // обнуляем приз
                    _prizeService.UpdateGamePrize(collisionObjId, true);
                }    
            }

            return car;
        }

        private Car Decorate(Car car, GameObjectType type)
        {
            switch (type)
            {
                case GameObjectType.Fuel:
                    {
                        if ((car.Fuel + 50) <= car.MaxFuel)
                        {
                            car = new FuelCarDecorator(car, car.Fuel + 100).GetCar();           
                        }
                        else
                        {
                            car = new FuelCarDecorator(car, car.MaxFuel).GetCar();
                        }

                        break;
                    }     
                case GameObjectType.Cartridge:
                    {
                        if ((car.Cartridges + 5) <= car.MaxCartridges)
                        {
                            car = new CatrigeCarDecorator(car, car.Cartridges + 5).GetCar();
                        }
                        else
                        {
                            car = new CatrigeCarDecorator(car, car.MaxCartridges).GetCar();
                        }

                        break;
                    }
                case GameObjectType.Tire:
                    car = new TireCarDecorator(car, true).GetCar();
                    break;
                default:
                    break;
            }

            return car;
        }

    }
}
