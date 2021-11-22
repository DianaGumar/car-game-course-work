using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace RaceGameLibrary
{
    abstract class CarDecorator : Car
    {
        //декорируемая машина
        protected Car car;
        //выполнил ли декоратор свою работу
        public bool IsDecorate { get; protected set; }

        public CarDecorator(Car car) : base()
        {
            this.car = car;
            IsDecorate = true;
        }
        //переопределяем все публичные методы и свойства на декорируемую машину
        public override bool Tire { get => car.Tire; set => car.Tire = value; }
        public override Key[] Keys => car.Keys; 
        public override float Fuel { get => car.Fuel; set => car.Fuel = value; }
        public override float Angle { get => car.Angle; set => car.Angle = value; }
        public override Vector2 Position { get => car.Position; set => car.Position = value; }
        public override Vector2 Size { get => car.Size; set => car.Size = value; }

        public override Vector2 Center => car.Center;

        public override float SpeedChange { get => car.SpeedChange; set => car.SpeedChange = value; }

        public override void Controlling(KeyboardState state)
        {
            car.Controlling(state);
        }

        public override void Draw()
        {
            car.Draw();
        }

        public override void Update()
        {
            car.Update();
        }
        //вернуть машину
        public Car GetCar()
        {
            return car;
        }


    }
}
