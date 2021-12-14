using RaceGame.Api.Common.GameObjects;
using OpenTK.Mathematics;
using System;

namespace RaceGame.Api.Services.MoveService
{
    public class MoveService : IMoveService
    {

        public MoveGameObject RotateLeft(MoveGameObject moveObject)
        {
            //поворот влево
            moveObject.Angle += 0.08f;

            return moveObject;
        }

        public MoveGameObject RotateRight(MoveGameObject moveObject)
        {
            //поворот вправо
            moveObject.Angle -= 0.08f;

            return moveObject;
        }

        public MoveGameObject MoveBack(MoveGameObject moveObject)
        {
            if (moveObject.Speed > -moveObject.MaxSpeed / 2 && moveObject.Fuel > 0 && moveObject.Tire) //вниз, движение назад
            {
                moveObject.Speed -= 0.04f;
                moveObject.Fuel -= Math.Abs(moveObject.Speed);
            }

            return moveObject;
        }

        public MoveGameObject MoveForward(MoveGameObject moveObject)
        {
            if (moveObject.Speed < moveObject.MaxSpeed && moveObject.Fuel > 0 && moveObject.Tire)  //вверх, вперед
            {
                moveObject.Speed += 0.04f;
                moveObject.Fuel -= Math.Abs(moveObject.Speed);
            }

            return moveObject;
        }

        public MoveGameObject UpdatePosition(MoveGameObject moveObject)
        {
            //движение вперед с поворотом
            var vector = Vector2.Transform(Vector2.UnitX,
                Quaternion.FromEulerAngles(0, 0, moveObject.Angle)) * (moveObject.Speed * moveObject.SpeedChange);

            moveObject.OldPositionX = moveObject.PositionX;
            moveObject.OldPositionY = moveObject.PositionY;

            moveObject.PositionX += vector.X;
            moveObject.PositionY += vector.Y;

            return moveObject;
        }

        public MoveGameObject ReturnPreviosState(MoveGameObject moveObject)
        {
            moveObject.PositionX = moveObject.OldPositionX;
            moveObject.PositionY = moveObject.OldPositionY;

            return moveObject;
        }
    }
}
