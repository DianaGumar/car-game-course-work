using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using OpenTK;

namespace RaceGameLibrary
{
    class Sprite : IDisposable
    {
        private int id;
        public Vector2 Size { get; private set; }

        public Sprite(int id, Vector2 size)
        {
            this.id = id;
            Size = size;
        }


        public void Draw(Vector2 position, Vector2 size, float angle, Vector2 center)
        {
            Vector2[] vertices = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 1),
            };

            GL.BindTexture(TextureTarget.Texture2D, id);
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.White);
            for (int i = 0; i < 4; i++)
            {
                GL.TexCoord2(vertices[i]);
                vertices[i].X *= Size.X;
                vertices[i].Y *= Size.Y;
                vertices[i] *= new Vector2(size.X / Size.X, size.Y / Size.Y);
                vertices[i] += position;
                vertices[i] = center + Vector2.Transform(vertices[i] - center, Quaternion.FromEulerAngles(0, 0, angle));
                GL.Vertex2(vertices[i]);
            }
            GL.End();
        }

        public void Dispose()
        {
            GL.DeleteTexture(id);
        }

        public static Sprite LoadSprite(string filePath)
        {
            filePath = "Sprite/" + filePath;
            if (!File.Exists(filePath)) throw new Exception("File not found!");
            Bitmap bmp = new Bitmap(filePath);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            return new Sprite(id, new Vector2(bmp.Height, bmp.Width));
        }
    }
}
