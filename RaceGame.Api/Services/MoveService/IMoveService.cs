using RaceGame.Api.Common.GameObjects;

namespace RaceGame.Api.Services.MoveService
{
    public interface IMoveService
    {
        public MoveGameObject RotateLeft(MoveGameObject moveObject);

        public MoveGameObject RotateRight(MoveGameObject moveObject);

        public MoveGameObject MoveBack(MoveGameObject moveObject);

        public MoveGameObject MoveForward(MoveGameObject moveObject);

        public MoveGameObject UpdatePosition(MoveGameObject moveObject);
    }
}
