using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RaceGameLibrary
{
    class GameObject
    {
        private Sprite sprite;

        public virtual float Angle { get; set; }
        public virtual Vector2 Position { get; set; }
        public virtual Vector2 Size {get;set;}
        public virtual Vector2 Center { get => Position + Size / 2; }

        protected GameObject()
        {
            sprite = null;
            Angle = 0f;
            Size = Vector2.Zero;
            Position = Vector2.Zero;
        }

        public GameObject(Vector2 position, Vector2 size, Sprite sprite) 
        {
            Position = position;
            Size = size;
            this.sprite = sprite;
        }

        public virtual void Update() { }

        public virtual void Draw()
        {
            sprite.Draw(Position, Size, Angle, Center);
        }
    }
}
