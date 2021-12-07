using RaceGame.Api.Common.GameObjects;
using RaceGame.Wpf.Client.NetworkServices;
using System;
using System.Collections.Generic;
using RaceGame.Wpf.Client.DrawServices;
using RaceGame.Api.Common.GameObjects.Car;
using System.Windows.Input;

namespace RaceGame.Wpf.Client.ClientState
{
    public class ClientStateService : IClientStateService
    {
        private readonly INetworkService _networkService;
        private readonly IDrawService _drawService;

        //private Dictionary<string, GameObject> _gameObjects;
        private List<GameObject> _gameObjects;
        private Car _gamer;
        private Car _enemyGamer;
        private bool isGamerCreated;

        public ClientStateService()
        {
            _networkService = new NetworkService();
            _drawService = new DrawService();
            //_gameObjects = new Dictionary<string, GameObject>();
            _gameObjects = new List<GameObject>();
        }

        // при старте клиента
        public bool ConnectClient()
        {
            var clientId = Guid.NewGuid().ToString();
            // connect gamer to game
            _gamer = _networkService.CreateGamer(clientId);
            //_gamer = (Car)_drawService.LoadSprite("car1.png", _gamer);

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
                // кидает запрос на получение обновлённого состояния игры
                _enemyGamer = _networkService.GetEnemyGamer(_gamer.Id);
                //var gameObjects = _networkService.GetGameObjects();

                //for (int i = 0; i < gameObjects.Count; i++)
                //{
                //    //gameObjects[i] = _drawService.LoadSprite("", gameObjects[i]);
                //    _gameObjects.Add(gameObjects[i].Id, gameObjects[i]);
                //}
            }
        }

        public void Draw()
        {
            if (isGamerCreated)
            {
                _drawService.Draw(_gamer);

                if (_enemyGamer != null)
                {
                    _drawService.Draw(_enemyGamer);
                }       

                // берёт существующие игровые объекты и отрисовывает
                foreach (var obj in _gameObjects)
                {
                    _drawService.Draw(obj);
                }
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
