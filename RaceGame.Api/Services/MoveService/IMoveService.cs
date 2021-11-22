using RaceGame.Api.Common.GameObjects;

namespace RaceGame.Api.Services.MoveService
{
    public interface IMoveService
    {
        public MoveGameObject MoveLeft(MoveGameObject moveObject);

        public MoveGameObject MoveRight(MoveGameObject moveObject);

        public MoveGameObject MoveBack(MoveGameObject moveObject);

        public MoveGameObject MoveForward(MoveGameObject moveObject);
    }
}
