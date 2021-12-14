using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using RaceGame.Common.Common;
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
        void UpdateGamerTexture(Car car);
        List<GameObject> GetLevel();
        GameObject[] GetPrizes();
        Point[] GetPrizesState();
        void ResetGame();
        List<GameObject> GetLevelRightSequence();
        Bullet[] GetBullets();
        void GetShot(string clientId);
    }
}
