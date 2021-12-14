using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using RaceGame.Api.Services.LevelService;
using RaceGame.Api.Services.MoveService;
using RaceGame.Api.Services.PrizeService;
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

            maxSpeed = 100;
            maxFuel = 300;
            startCartridges = 10;
        }

        public Car CreateCar(string clientId)
        {
            float positionX = 110;
            float positionY = 210;

            float sizeX = 40;
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
                LevelsSequence = new int[9]
            };

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
            car = CheckAndUpdateWithPrizeCollision(car, _prizeService.GetGamePrizes());

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

        private Car CreckAndUpdateLevelsSequense(Car car, string levelId)
        {
            //gameObject.RightLevelsSequence[]

            return car;
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
