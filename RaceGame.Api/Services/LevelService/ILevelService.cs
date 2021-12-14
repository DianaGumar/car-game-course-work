using RaceGame.Api.Common.GameObjects;

namespace RaceGame.Api.Services.LevelService
{
    public interface ILevelService
    {
        public void CreateLevel();

        public GameObject[] GetLevel();

        public GameObject[] GetLevelRightSequensce();
    }
}
