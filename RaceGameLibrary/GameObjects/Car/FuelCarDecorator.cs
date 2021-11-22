using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceGameLibrary
{
    class FuelCarDecorator : CarDecorator
    {
        public FuelCarDecorator(Car car) : base(car)
        {
            car.Fuel += 10f;
            IsDecorate = false;
        }
    }
}
