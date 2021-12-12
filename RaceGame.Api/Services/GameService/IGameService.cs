using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using System.Collections.Generic;

namespace RaceGame.Api.Services.GameService
{
    public interface IGameService
    {
        public Car AddGamer(string clientId);

        public List<GameObject> GetAllObjects();

        public void ResetGame();
    }
}
