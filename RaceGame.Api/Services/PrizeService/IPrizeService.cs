using RaceGame.Api.Common.GameObjects;
using RaceGame.Common.Common;

namespace RaceGame.Api.Services.PrizeService
{
    public interface IPrizeService
    {
        GameObject[] GetGamePrizes();

        Point[] GetPrizesState();

        void RefreshPrizes(GameObject[] objects);

        void UpdateGamePrize(string priseId, bool isDeactivate);
    }
}
