using OpenTK.Mathematics;
using RaceGame.Api.Common.GameObjects;

namespace RaceGame.Api.Services.MoveService
{
    public static class CollisionHelper
    {
        // проверка на коллизию со свеми объектами
        public static bool CheckCollision(GameObject gameObject, 
            out string collizeObjectId, params GameObject[] gameObjects)
        {
            var collision = false;
            collizeObjectId = null;

            foreach (var obj in gameObjects)
            {
                if (obj != null && !obj.IsDeactivate && !obj.Id.Equals(gameObject.Id))
                {
                    // проверка на коллизию разделяющей прямой
                    collision = collision || AABBAndAABBB(
                        new Vector2(gameObject.PositionX, gameObject.PositionY),
                        new Vector2(gameObject.SizeX, gameObject.SizeY),
                        new Vector2(obj.PositionX, obj.PositionY),
                        new Vector2(obj.SizeX, obj.SizeY));
                    if (collision)
                    {
                        collizeObjectId = obj.Id;
                        return collision;
                    }
                }
            }

            return collision;

            //if (enemy != null)
            //{
            //    // проверка на коллизию разделяющей прямой
            //    collision = collision || CollisionHelper.AABBAndAABBB(
            //    new Vector2(gameObject.PositionX, gameObject.PositionY),
            //    new Vector2(gameObject.SizeX, gameObject.SizeY),
            //    new Vector2(enemy.PositionX, enemy.PositionY),
            //    new Vector2(enemy.SizeX, enemy.SizeY));

            //    if (collision)
            //    {
            //        return collision;
            //    }
            //}
        }

        // коллизия габаритов
        public static bool MacroCollision(GameObject obj1, GameObject obj2)
        {
            var XColl = false;
            var YColl = false;

            if ((obj1.PositionX + obj1.SizeX >= obj2.PositionX) &&
                (obj1.PositionX <= obj2.PositionX + obj2.SizeX)) XColl = true;
            if ((obj1.PositionY + obj1.SizeY >= obj2.PositionY) &&
                (obj1.PositionY <= obj2.PositionY + obj2.SizeY)) YColl = true;

            if (XColl & YColl) { return true; }
            return false;
        }

        public static bool AABBAndAABBB(Vector2 b1position, Vector2 b1size, 
            Vector2 b2position, Vector2 b2size)
        {
            Vector2[] axesToTest = new Vector2[] 
            {
                new Vector2(1, 0), new Vector2(0, 1),
            };

            var b1Min = getMin(b1position, b1size);
            var b1Max = getMax(b1position, b1size);

            var b2Min = getMin(b2position, b2size);
            var b2Max = getMax(b2position, b2size);

            for (int i = 0; i < axesToTest.Length; i++)
            {
                if (!overlapOnAxis(b1Min, b1Max, b2Min, b2Max, axesToTest[i]))
                {
                    return false;
                }
            }
            return true;

        }

        private static bool overlapOnAxis(Vector2 b1Min, Vector2 b1Max, Vector2 b2Min, Vector2 b2Max, Vector2 axis)
        {
            Vector2 interval1 = getInterval(b1Min, b1Max, axis);
            Vector2 interval2 = getInterval(b2Min, b2Max, axis);
            return ((interval2.X <= interval1.Y) && (interval1.X <= interval2.Y));
        }

        private static Vector2 getInterval(Vector2 min, Vector2 max, Vector2 axis)
        {
            Vector2 result = new Vector2(0, 0);

            //Vector2 min = rect.getMin();
            //Vector2 max = rect.getMax();

            Vector2[] vertices = new Vector2[] {
            new Vector2(min.X, min.Y), new Vector2(min.X, max.Y),
            new Vector2(max.X, min.Y), new Vector2(max.X, max.Y) };

            result.X = dot(axis, vertices[0]);
            result.Y = result.X;
            for (int i = 1; i < 4; i++)
            {
                float projection = dot(axis, vertices[i]);
                if (projection < result.X)
                {
                    result.X = projection;
                }
                if (projection > result.Y)
                {
                    result.Y = projection;
                }
            }
            return result;
        }

        // скалярное произведение векторов
        public static float dot(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        // вычитание
        public static Vector2 sub(Vector2 v1, Vector2 v2)
        {
            v1.X = v1.X - v2.X;
            v1.Y = v1.Y - v2.Y;

            return v1;
        }

        // прибавление
        public static Vector2 add(Vector2 v1, Vector2 v2)
        {
            v1.X = v1.X + v2.X;
            v1.Y = v1.Y + v2.Y;

            return v1;
        }

        // умножение на скаляр
        public static Vector2 mul(Vector2 v1, float scalar)
        {
            v1.X = v1.X * scalar;
            v1.Y = v1.Y * scalar;

            return v1;
        }

        public static Vector2 getMin(Vector2 position, Vector2 size)
        {
            var halfSize = CollisionHelper.mul(size, 0.5f);
            return CollisionHelper.sub(position, halfSize);
        }

        public static Vector2 getMax(Vector2 position, Vector2 size)
        {
            var halfSize = CollisionHelper.mul(size, 0.5f);
            return CollisionHelper.add(position, halfSize);
        }
    }
}
