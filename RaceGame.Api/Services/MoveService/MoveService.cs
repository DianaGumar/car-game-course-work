using RaceGame.Api.Common.GameObjects;

namespace RaceGame.Api.Services.MoveService
{
    public class MoveService : IMoveService
    {
        public MoveGameObject DownSpeed(MoveGameObject moveObject)
        {
            moveObject.Speed -= moveObject.SpeedChange;
            return moveObject;
        }

        public MoveGameObject UpSpeed(MoveGameObject moveObject)
        {
            moveObject.Speed += moveObject.SpeedChange;
            return moveObject;
        }

        public MoveGameObject MoveLeft(MoveGameObject moveObject)
        {
            moveObject.PositionX -= moveObject.Speed;
            return moveObject;
        }

        public MoveGameObject MoveRight(MoveGameObject moveObject)
        {
            moveObject.PositionX -= moveObject.Speed;
            return moveObject;
        }

        public MoveGameObject MoveBack(MoveGameObject moveObject)
        {
            moveObject.PositionX -= moveObject.Speed;
            return moveObject;
        }

        public MoveGameObject MoveForward(MoveGameObject moveObject)
        {
            moveObject.PositionX += moveObject.Speed;
            return moveObject;
            //Position += Vector2.Transform(Vector2.UnitX, Quaternion.FromEulerAngles(0, 0, Angle)) * (speed * SpeedChange); //движение вперед с поворотом
        }
    }
}
