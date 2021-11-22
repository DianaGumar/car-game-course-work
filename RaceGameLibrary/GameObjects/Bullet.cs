using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RaceGameLibrary
{
    class Bullet : GameObject
    {
        protected Vector2 direction;
        public float Radius { get; protected set; }

        public Bullet(Vector2 position, float size, Vector2 direction, Sprite sprite) : base(position, new Vector2(size), sprite)
        {
            Radius = size / 2;
            this.direction = direction;
        }

        public override void Update()
        {
            Position += direction * 100f;
        }

    }
}
