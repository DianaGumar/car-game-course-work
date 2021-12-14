using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.Prize.Factory;
using RaceGame.Api.Services.GameService;
using RaceGame.Common.Common;
using System;
using System.Linq;

namespace RaceGame.Api.Services.PrizeService
{
    public class PrizeService : IPrizeService
    {
        private GameObject[] _gamePrizes;
        private int _prizeSize;
        private int _prizeCount;

        private readonly FuelPrizeFactory _fuelPrizeFactory;
        private readonly CartridgePrizeFactory _cartridgePrizeFactory;
        private readonly TirePrizeFactory _tirePrizeFactory;

        public PrizeService()
        {
            _tirePrizeFactory = new TirePrizeFactory();
            _cartridgePrizeFactory = new CartridgePrizeFactory();
            _fuelPrizeFactory = new FuelPrizeFactory();

            _prizeSize = 20;
            CreatePrizes();
        }

        private void CreatePrizes()
        {
            // создаём призы
            _gamePrizes = new GameObject[]
            {
                _fuelPrizeFactory.GetObject(_prizeSize, _prizeSize),
                _fuelPrizeFactory.GetObject(_prizeSize, _prizeSize),
                _fuelPrizeFactory.GetObject(_prizeSize, _prizeSize),
                _cartridgePrizeFactory.GetObject(_prizeSize, _prizeSize),
                _cartridgePrizeFactory.GetObject(_prizeSize, _prizeSize),
                _tirePrizeFactory.GetObject(_prizeSize, _prizeSize),
                _tirePrizeFactory.GetObject(_prizeSize, _prizeSize),
                _tirePrizeFactory.GetObject(_prizeSize, _prizeSize),
            };

            _prizeCount = _gamePrizes.Length;

            for (int i = 0; i < _gamePrizes.Length; i++)
            {
                _gamePrizes[i].IsDeactivate = true;
                _gamePrizes[i].Id = Guid.NewGuid().ToString();
            }
        }

        public void UpdateGamePrize(string priseId, bool isDeactivate)
        {
            var prize = _gamePrizes.FirstOrDefault(p => p.Id.Equals(priseId));

            if(prize != null)
            {
                prize.IsDeactivate = isDeactivate;
            }
        }

        public GameObject[] GetGamePrizes()
        {
            return _gamePrizes;
        }

        public Point[] GetPrizesState()
        {
            // возвращать только список координат
            Point[] points = new Point[_prizeCount];

            for (int i = 0; i < _prizeCount; i++)
            {
                points[i] = new Point()
                {
                    PositionX = _gamePrizes[i].PositionX,
                    PositionY = _gamePrizes[i].PositionY,
                    IsDeactivate = _gamePrizes[i].IsDeactivate,
                };
            }

            return points;
        }

        // генерируем призам новые позиции и устанавливаем флаг - активен
        public void RefreshPrizes(GameObject[] objects)
        {
            for (int i = 0; i < _gamePrizes.Length; i++)
            {
                _gamePrizes[i].IsDeactivate = false;
                _gamePrizes[i] = PositionHelper.RandomNoCollizionPosition(_gamePrizes[i], objects);
            }
        }

        public GameObject GetGamePrize(string id)
        {
            return _gamePrizes.FirstOrDefault(p => p.Id.Equals(id));
        }
    }
}
