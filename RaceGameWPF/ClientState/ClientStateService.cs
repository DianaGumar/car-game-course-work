using RaceGame.Api.Common.GameObjects;
using RaceGame.Wpf.Client.NetworkServices;
using System;
using System.Collections.Generic;
using RaceGame.Wpf.Client.DrawServices;
using RaceGame.Api.Common.GameObjects.Car;
using System.Windows.Input;
using System.Drawing;
using System.Threading;
using System.Windows;

namespace RaceGame.Wpf.Client.ClientState
{
    public class ClientStateService : IClientStateService
    {
        private readonly INetworkService _networkService;
        private readonly IDrawService _drawService;

        private GameObject _bg;
        private GameObject[] _gamePrizes;
        private List<GameObject> _level;
        private List<GameObject> _levelRightSequence;
        private Car _gamer;
        private Car _enemyGamer;
        private bool isGamerCreated;

        private int _enemyTextureId;
        private int _tireTextureId;
        private int _fuelTextureId;
        private int _cartriggeTextureId;
        private int _bgTextureId;

        Timer timer;

        public ClientStateService()
        {
            _networkService = new NetworkService();
            _drawService = new DrawService();

            _bg = new GameObject()
            {
                PositionY = 315,
                PositionX = 540,
                SizeX = 1080,
                SizeY = 630,
            };
        }

        public bool IsWon()
        {
            if (_enemyGamer == null)
            {
                return false;
            }

            return _gamer.RightLevelsSequence >= 2 || _enemyGamer.RightLevelsSequence >= 2;
        }

        public bool IsYouWon()
        {
            return _gamer.RightLevelsSequence >= 2;
        }

        public void ResetGame()
        {
            _networkService.ResetGame();
        }

        private void UpdatePrizes(object obj)
        {
            //_gamePrizes = _networkService.GetPrizes();

            //получаем игровые призовые объекты.
            var state = _networkService.GetPrizesState();
            for (int i = 0; i < _gamePrizes.Length; i++)
            {
                _gamePrizes[i].PositionX = state[i].PositionX;
                _gamePrizes[i].PositionY = state[i].PositionY;
                _gamePrizes[i].IsDeactivate = state[i].IsDeactivate;
            }
        }

        public void GetGameObjects()
        {
            _level = _networkService.GetLevel();
            _levelRightSequence = _networkService.GetLevelRightSequence();
            _gamePrizes = _networkService.GetPrizes();

            // таймер на обновлнеие координат - устанавливаем метод обратного вызова
            // создаем таймер - на каждые n сек
            //timer = new Timer(new TimerCallback(UpdatePrizes), null, 0, 20000);
        }

        // при старте клиента
        public bool ConnectClient()
        {
            var clientId = Guid.NewGuid().ToString();
            // connect gamer to game
            _gamer = _networkService.CreateGamer(clientId);

            // добавляем игроку текстуру
            float height = 0;
            float width = 0;
            _gamer.SpriteId = _drawService.LoadSprite("car1.png", out height, out width);
            _gamer.SpriteSizeX = width;
            _gamer.SpriteSizeY = height;
            _networkService.UpdateGamerTexture(_gamer);

            //// загружаем текстуры других объектов
            ///
            _enemyTextureId = _drawService.LoadSprite("car2.png", out height, out width);
            _cartriggeTextureId = _drawService.LoadSprite("patron.png", out height, out width);
            _tireTextureId = _drawService.LoadSprite("shina.png", out height, out width);
            _fuelTextureId = _drawService.LoadSprite("health.png", out height, out width);
            _bg.SpriteId = _drawService.LoadSprite("RACE2.png", out height, out width);
            _bgTextureId = _drawService.LoadSprite("valun.png", out height, out width);


            isGamerCreated = _gamer != null;

            return isGamerCreated;
        }

        public void ClientAction(Key key)
        {
            var direction = KeyToCode(key);

            _gamer = _networkService.MoveGamer(_gamer.Id, direction);
        }

        // в n-ый промежуток времени
        public void Update()
        {
            if(isGamerCreated)
            {
                // игрок продолжает движение на заданной скорости
                _gamer = _networkService.MoveGamer(_gamer.Id, 0);

                // работа с колизией происходит на стороне сервера
                // кидает запрос на получение противника
                _enemyGamer = _networkService.GetEnemyGamer(_gamer.Id);

                UpdatePrizes(null);

                //if (_gamer.PrizeId != null)
                //{
                //    // убираем из видимости этот приз
                //    for (int i = 0; i < _gamePrizes.Length; i++)
                //    {
                //        if (!_gamePrizes[i].IsDeactivate && _gamePrizes[i].Id.Equals(_gamer.PrizeId))
                //        {
                //            _gamePrizes[i].IsDeactivate = true;
                //            break;
                //        }
                //    }                    
                //}
            }
        }

        public void Draw()
        {
            if (isGamerCreated)
            {
                _drawService.Draw(_bg, Color.White, _bg.SpriteId);

                for (int i = 0; i < _levelRightSequence.Count; i++)
                {
                    _drawService.Draw(_levelRightSequence[i], Color.White, 0);
                }

                //for (int i = 0; i < _level.Count(); i++)
                //{
                //    _drawService.Draw(_level[i], Color.LightGray, _bgTextureId);
                //}

                // берёт существующие игровые объекты и отрисовывает
                foreach (var obj in _gamePrizes)
                {
                    if (!obj.IsDeactivate)
                    {
                        if (obj.Name == GameObjectType.Cartridge)
                        {
                            _drawService.Draw(obj, Color.White, _cartriggeTextureId);
                        }
                        else if (obj.Name == GameObjectType.Tire)
                        {
                            _drawService.Draw(obj, Color.White, _tireTextureId);
                        }
                        else if (obj.Name == GameObjectType.Fuel)
                        {
                            _drawService.Draw(obj, Color.White, _fuelTextureId);
                        }
                        else
                        {
                            _drawService.Draw(obj, Color.Yellow, 0);
                        }
                    }
                }    

                if (_enemyGamer != null)
                {
                    _drawService.Draw(_enemyGamer, Color.White, _enemyTextureId);
                }

                if (_gamer.IsCollizion)
                {
                    _drawService.Draw(_gamer, Color.Red, _gamer.SpriteId);
                }
                else
                {
                    _drawService.Draw(_gamer, Color.White, _gamer.SpriteId);
                }

                // отрисовка показателей игрока
                _drawService.DrawState(_gamer);
            }
        }

        // действия игрока
        private int KeyToCode(Key key)
        {
            int code = 1;

            switch (key)
            {
                case Key.W:
                    code = 1;
                    break;
                case Key.S:
                    code = 2;
                    break;
                case Key.A:
                    code = 3;
                    break;
                case Key.D:
                    code = 4;
                    break;
                default:
                    break;
            }

            return code;
        }

        public void EndGame()
        {
            _networkService.DeleteGamer(_gamer.Id);
        }
    }
}
