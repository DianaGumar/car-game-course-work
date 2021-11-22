using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RaceGameLibrary
{
    class BulletPrize : Prize
    {
        public BulletPrize(Vector2 position, float size, Sprite sprite) : base(position, size, sprite)
        {
        }

        public override Car UpgradeCar(Car car)
        {
            return new BulletCarDecorator(car, Sprite.LoadSprite(""));
        }
    }
}
