using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RaceGameLibrary
{
    class FuelPrize : Prize
    {
        public FuelPrize(Vector2 position, float size, Sprite sprite) : base(position, size, sprite)
        {
        }

        public override Car UpgradeCar(Car car)
        {
            return new FuelCarDecorator(car);
        }
    }
}
