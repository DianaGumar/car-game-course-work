using RaceGame.Api.Common.GameObjects;

namespace RaceGame.Api.Services.MoveService
{
    public class MoveService : IMoveService
    {
        public MoveGameObject DownSpeed(MoveGameObject moveObject)
        {
            //if (state.IsKeyDown(controls[0]) && speed < maxSpeed)  //вверх, вперед
            //{
            //    speed += 0.02f;
            //}
            //if (state.IsKeyDown(controls[1]) && speed > -maxSpeed / 2) //вниз, движение назад
            //{
            //    speed -= 0.02f;
            //}
            //if (state.IsKeyDown(controls[2])) //поворот вправо
            //{
            //    Angle += 0.02f;
            //}
            //if (state.IsKeyDown(controls[3])) //поворот влево
            //{
            //    Angle -= 0.02f;
            //}

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
