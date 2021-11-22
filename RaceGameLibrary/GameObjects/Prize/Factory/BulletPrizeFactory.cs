using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RaceGameLibrary
{
    class BulletPrizeFactory : PrizeFactory
    {
        public BulletPrizeFactory(float size, Sprite sprite) : base(size, sprite)
        {
        }

        public override Prize GetPrize(Vector2 position)
        {
            return new BulletPrize(position, size, sprite);
        }
    }
}
