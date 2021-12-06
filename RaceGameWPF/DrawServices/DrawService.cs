﻿using RaceGame.Api.Common.GameObjects;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using System.IO;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using System.Drawing;
using OpenTK;

namespace RaceGame.Wpf.Client.DrawServices
{
    public class DrawService : IDrawService, IDisposable
    {
        private readonly List<int> ids;

        public DrawService()
        {
            ids = new List<int>();
        }

        // передаём объект KeyValuePair<int, Vector2> sprite
        public void Draw(GameObject obj)
        {
            var position = new Vector2(obj.PositionX, obj.PositionY);
            var size = new Vector2(obj.SizeX, obj.SizeY);
            var Size = size; // new Vector2(obj.SpriteSizeX, obj.SpriteSizeY);
            Vector2 center = position + size / 2;

            Vector2[] vertices = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 1),
            };

            //GL.BindTexture(TextureTarget.Texture2D, obj.SpriteId);
            GL.Begin(PrimitiveType.Quads);
            
            GL.Color3(Color.White);
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
        public GameObject LoadSprite(string filePath, GameObject obj)
        {
            filePath = "Sprite/" + filePath;
            if (!File.Exists(filePath)) throw new Exception("File not found!");
            Bitmap bmp = new Bitmap(filePath);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), 
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, 
                TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, 
                TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            // take in memory all ids for dispose option
            ids.Add(id);

            obj.SpriteId = id;
            obj.SpriteSizeX = bmp.Height;
            obj.SpriteSizeY = bmp.Width;

            return obj;
            // return id with size-vector
            //return new KeyValuePair<int, Vector2>(id, new Vector2(bmp.Height, bmp.Width));
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