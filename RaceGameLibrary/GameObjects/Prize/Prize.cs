using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RaceGameLibrary
{
    abstract class Prize : GameObject
    {
        public float Radius {get; protected set;}

        public Prize(Vector2 position, float size, Sprite sprite) : base(position, new Vector2(size), sprite)
        {
            Radius = size;
        }

        public abstract Car UpgradeCar(Car car);
    }
}
