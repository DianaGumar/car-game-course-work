using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceGameLibrary
{
    abstract class PrizeFactory
    {
        protected Sprite sprite;
        protected float size;
        public PrizeFactory(float size, Sprite sprite)
        {
            this.sprite = sprite;
            this.size = size; 
        }

        public abstract Prize GetPrize(Vector2 position);
    }
}
