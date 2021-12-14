using RaceGame.Api.Common.GameObjects;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using System.IO;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using System.Drawing;
using OpenTK;
using RaceGame.Api.Common.GameObjects.Car;

namespace RaceGame.Wpf.Client.DrawServices
{
    public class DrawService : IDrawService, IDisposable
    {
        private readonly List<int> ids;

        public DrawService()
        {
            ids = new List<int>();
        }

        public void DrawCircle(float x, float y, float radius, Color Color)
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);

            //GL.Enable(EnableCap.Blend);
            //GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Begin(PrimitiveType.TriangleFan);
            GL.Color3(Color);

            GL.Vertex2(x, y);
            for (int i = 0; i < 360; i++)
            {
                GL.Vertex2(x + Math.Cos(i) * radius, y + Math.Sin(i) * radius);
            }

            GL.End();
            //GL.Disable(EnableCap.Blend);
        }

        public void DrawRectangle(Vector2 Position, Vector2 Size, Color Color)
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color);
            GL.Vertex3(Position.X, Position.Y, 0);
            GL.Vertex3(Position.X + Size.X, Position.Y, 0);
            GL.Vertex3(Position.X + Size.X, Position.Y + Size.Y, 0);
            GL.Vertex3(Position.X, Position.Y + Size.Y, 0);

            GL.End();
        }

        public void DrawEmptyRectangle(Vector2 Position, Vector2 Size, Color color)
        {
            GL.LineWidth(3.5f);
            
            GL.Begin(PrimitiveType.LineStrip);

            GL.Color3(color);
            GL.Vertex3(Position.X, Position.Y, 0);
            GL.Vertex3(Position.X + Size.X, Position.Y, 0);
            GL.Vertex3(Position.X + Size.X, Position.Y + Size.Y, 0);
            GL.Vertex3(Position.X, Position.Y + Size.Y, 0);
            GL.Vertex3(Position.X, Position.Y, 0);

            GL.End();
        }

        public void DrawState(Car obj)
        {
            // сбрасываем отрисовку текстур
            GL.BindTexture(TextureTarget.Texture2D, 0);

            DrawRectangle(new Vector2(50, 20), new Vector2((obj.Fuel*100)/obj.MaxFuel, 20), Color.Green);
            DrawEmptyRectangle(new Vector2(50, 20), new Vector2(100, 20), Color.Black);

            // отрисовка патронов
            DrawRectangle(new Vector2(50, 50), new Vector2((obj.Cartridges*100)/obj.MaxCartridges, 20), Color.Red);
            DrawEmptyRectangle(new Vector2(50, 50), new Vector2(100, 20), Color.Black);

            // отрисовка шин
            DrawRectangle(new Vector2(50, 80), new Vector2(obj.Tire ? 100 : 0, 20), Color.Black);
            DrawEmptyRectangle(new Vector2(50, 80), new Vector2(100, 20), Color.Black);
        }

        // передаём объект KeyValuePair<int, Vector2> sprite
        public void Draw(GameObject obj, Color color, int textureId)
        {
            var position = new Vector2(obj.PositionX - obj.SizeX/2, obj.PositionY - obj.SizeY/2);
            var size = new Vector2(obj.SizeX, obj.SizeY);
            var Size = size; //new Vector2(obj.SpriteSizeX, obj.SpriteSizeY);

            Vector2 center = position + size / 2;

            Vector2[] vertices = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 1),
            };

            //GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.Begin(PrimitiveType.Quads);
            
            GL.Color3(color);
            for (int i = 0; i < 4; i++)
            {
                GL.TexCoord2(vertices[i]);
                vertices[i].X *= Size.X;
                vertices[i].Y *= Size.Y;
                vertices[i] *= new Vector2(size.X / Size.X, size.Y / Size.Y);
                vertices[i] += position;
                vertices[i] = center + Vector2.Transform(vertices[i] - center, 
                    Quaternion.FromEulerAngles(0, 0, obj.Angle));
                GL.Vertex2(vertices[i]);
            }

            GL.End();
        }

        // loads img into GL collection and into Game object
        public int LoadSprite(string filePath, out float height, out float width)
        {
            filePath = "E:/course_work/New folder/car-game-course-work/RaceGameWPF/Sprite/" + filePath;
            if (!File.Exists(filePath)) throw new Exception("File not found!");
            Bitmap bmp = new Bitmap(filePath);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), 
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            //GL.Enable(EnableCap.Texture2D);

            int tex;
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out tex);
            GL.BindTexture(TextureTarget.Texture2D, tex);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            // создание mipmap для облегчённого рендеринга - умньшает размер текстуры
            //GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            // take in memory all ids for dispose option
            ids.Add(tex);

            height = bmp.Height;
            width = bmp.Width;

            return tex;
        }

        public void Dispose()
        {
            foreach(var id in ids)
            {
                GL.DeleteTexture(id);
            }
        }
    }
}
