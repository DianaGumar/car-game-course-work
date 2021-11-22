using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RaceGameLibrary
{
    class RectangleCollision
    {
        private Vector2 position;
        private Vector2 size;

        public RectangleCollision(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
        }

        ////принадлежит ли точка прямоугольнику

        public bool CheckPosition(float x, float y)
        {
            return position.X < x && position.X + size.X > x && position.Y < y && position.Y + size.Y > y;
        }

        //проверка на коллизию
        //public void Draw()
        //{
        //    Vector2[] vertices = new Vector2[4]
        //    {
        //        new Vector2(0, 0),
        //        new Vector2(1, 0),
        //        new Vector2(1, 1),
        //        new Vector2(0, 1),
        //    };
        //    GL.Begin(PrimitiveType.Quads);
        //    GL.Color4(Color.Blue);
        //    for (int i = 0; i < 4; i++)
        //    {

        //        vertices[i].X *= size.X;
        //        vertices[i].Y *= size.Y;
        //        vertices[i] += position;

        //        GL.Vertex2(vertices[i]);
        //    }
        //    GL.End();
        //}
    }
}
