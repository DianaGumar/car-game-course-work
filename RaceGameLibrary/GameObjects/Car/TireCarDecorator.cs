using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceGameLibrary
{
    class TireCarDecorator : CarDecorator
    {
        public TireCarDecorator(Car car) : base(car)
        {
            car.Tire = true;
            IsDecorate = false;
        }
    }
}
