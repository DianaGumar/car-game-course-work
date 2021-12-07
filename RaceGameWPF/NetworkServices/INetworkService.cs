using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using System.Collections.Generic;

namespace RaceGame.Wpf.Client.NetworkServices
{
    public interface INetworkService
    {
        List<GameObject> GetGameObjects(string gamerId);

        Car CreateGamer(string clientId);

        Car GetEnemyGamer(string currentGamerId);

        Car MoveGamer(string gamerId, int direction);

        void DeleteGamer(string clientId);
    }
}
