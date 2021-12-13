using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using RaceGame.Common.Common;

namespace RaceGame.Api.Services.PrizeService
{
    public interface IPrizeService
    {
        GameObject[] GetGamePrizes();

        Point[] GetPrizesState();

        void RefreshPrizes(GameObject[] objects);

        void UpdateGamePrize(string priseId, bool isDeactivate);

        GameObject GetGamePrize(string id);
    }
}
