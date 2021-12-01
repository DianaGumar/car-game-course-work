using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using RaceGame.Wpf.Client.NetworkServices;
using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Input;
using System.Windows.Input;

namespace RaceGame.Wpf.Client.ClientState
{
    public class ClientStateService : IClientStateService
    {
        private readonly INetworkService _networkService;

        private List<GameObject> _gameState;
        private Car _gamer; // нужно ли?

        public ClientStateService()
        {
            _networkService = new NetworkService();
            _gameState = new List<GameObject>();
        }

        public void ConnectClient()
        {
            var clientId = Guid.NewGuid().ToString();
            // connect gamer to game
            _gamer = _networkService.CreateGamer(clientId).Result;

            // get game state
            _gameState = _networkService.GetGameObjects().Result;
        }

        public void Update()
        {
            //var w = Keyboard.GetKeyStates(Key.W);
            //var s = Keyboard.GetKeyStates(Key.S);
            //var d = Keyboard.GetKeyStates(Key.D);
            //var a = Keyboard.GetKeyStates(Key.A);


            // работа с колизией происходит на стороне сервера

            //_gameState = _networkService.MoveGamer();

            _gameState = _networkService.GetGameObjects().Result;

            //Car1.Controlling(Keyboard.GetKeyStates());
            //Collision.CheckLevelCollision(Level, Car1);
        }

        public void Draw()
        {
            // берёт существующие объекты и отрисовывает
            throw new NotImplementedException();
        }
    }
}
