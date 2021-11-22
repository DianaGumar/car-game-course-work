using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RaceGameLibrary
{
    class Level : GameObject
    {
        //массив прямоугольников, которые образуют дорогу
        private RectangleCollision[] rectangleCollisions;

        public Level(Vector2 position, Vector2 size, Sprite sprite, int count, Vector2[] positions, Vector2[] sizes):base(position, size ,sprite)
        {
            rectangleCollisions = new RectangleCollision[count];
            for (int i = 0; i < count; i++)
            {
                rectangleCollisions[i] = new RectangleCollision(positions[i], sizes[i]);
            }
        }
        //поиск коллизий
        public bool SearchCollision(float x, float y)
        {
            bool collision = false;
            for (int i = 0; i < rectangleCollisions.Length; i++)
            {
                collision = collision || rectangleCollisions[i].CheckPosition(x, y);
            }
            return collision;
        }

        //хвостик проверки на коллизию
        //public override void Draw()
        //{
        //    base.Draw();
        //    for (int i = 0; i < rectangleCollisions.Length; i++)
        //    {
        //        rectangleCollisions[i].Draw();
        //    }
        //}
    }
}
