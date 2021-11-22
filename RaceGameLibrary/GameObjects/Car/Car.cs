using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace RaceGameLibrary
{
    class Car : GameObject
    {
        protected Key[] controls;
        protected float maxSpeed;
        protected float speed;
        protected float maxFuel;

        public virtual bool Tire { get; set; }
        public virtual Key[] Keys { get => controls; }
        public virtual float Fuel { get; set; }
        public virtual float SpeedChange { get; set; }

        protected Car() : base() { }
        public Car(Vector2 position, Vector2 size, Sprite sprite, Key[] controls) : base(position, size, sprite) 
        {
            maxSpeed = 100;
            speed = 0;
            maxFuel = 100;
            Fuel = maxFuel;
            SpeedChange = 1;
            this.controls = controls;
            Tire = true;
        }

        public virtual void Controlling(KeyboardState state)
        {
            if(state.IsKeyDown(controls[0]) && speed < maxSpeed)  //вверх, вперед
            {
                speed += 0.02f;
            }
            if (state.IsKeyDown(controls[1]) && speed >  -maxSpeed / 2) //вниз, движение назад
            {
                speed -= 0.02f;
            }
            if (state.IsKeyDown(controls[2])) //поворот вправо
            {
                Angle += 0.02f;
            }
            if (state.IsKeyDown(controls[3])) //поворот влево
            {
                Angle -= 0.02f;
            }
        }



        public override void Update()
        {
            Position += Vector2.Transform(Vector2.UnitX, Quaternion.FromEulerAngles(0, 0, Angle)) * (speed * SpeedChange); //движение вперед с поворотом
        }
    }
}
