using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace RaceGameLibrary
{
    class BulletCarDecorator : CarDecorator
    {
        private Sprite bulletSprite;
        private int amountBullets;
        private long timeShot;
        public List<Bullet> Bullets { get; set; }



        public BulletCarDecorator(Car car, Sprite bulletSprite) : base(car)
        {
            this.bulletSprite = bulletSprite;
            amountBullets = 10;
            timeShot = 0;
        }

        public override void Controlling(KeyboardState state)
        {
            if(state.IsKeyDown(car.Keys[4]) && timeShot + 1000 < DateTime.Now.Ticks && amountBullets <= 0)
            {
                Bullets.Add(new Bullet(Position, 10, Vector2.Transform(Vector2.UnitX, Quaternion.FromEulerAngles(0, 0, car.Angle)), bulletSprite));
                amountBullets--;
                timeShot = DateTime.Now.Ticks;
            }
            car.Controlling(state);
        }

        public override void Update()
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                Bullets[i].Update();
                if(Bullets[i].Position.X < 0 || Bullets[i].Position.X > 1440 || Bullets[i].Position.Y < 0 || Bullets[i].Position.X > 800)
                {
                    Bullets.RemoveAt(i);
                    i--;
                }
            }
            if (amountBullets <= 0 && Bullets.Count == 0) IsDecorate = false;
            car.Update();
        }

        public override void Draw()
        {
            base.Draw();
            foreach (Bullet bullet in Bullets)
            {
                bullet.Draw();
            }
        }
    }
}
