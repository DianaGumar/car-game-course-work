using RaceGame.Api.Common.GameObjects.Car;

namespace RaceGame.Api.Services.GameService
{
    public interface IGameService
    {
        public Car AddGamer(string clientId);

        public Car MoveGamer(string clientId, string gameObjectId, int direction);
    }
}
