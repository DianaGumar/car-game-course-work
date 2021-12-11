using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using System.Collections.Generic;

namespace RaceGame.Api.Services.GameService
{
    public interface IGameService
    {
        public Car AddGamer(string clientId);

        public Car MoveGamer(string clientId, int direction);

        public void DeleteGamer(string clientId);

        public List<GameObject> GetGameObjects(string gamerId);

        public List<GameObject> GetGameObjects();

        public List<Car> GetAllGamers();

        public GameObject[] GetLevel();
    }
}
