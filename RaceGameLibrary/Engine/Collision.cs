using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceGameLibrary
{
    static class Collision
    {
        public static void CheckLevelCollision(Level level, Car car)
        {
            if (!level.SearchCollision(car.Center.X, car.Center.Y) )
            {
                car.SpeedChange = 0.2f;
            }
            else car.SpeedChange = 1f; 
        }





    }
}
