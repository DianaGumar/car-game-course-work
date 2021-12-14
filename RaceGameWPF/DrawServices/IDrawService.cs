using OpenTK;
using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using System.Drawing;

namespace RaceGame.Wpf.Client.DrawServices
{
    public interface IDrawService
    {
        // отрисовка показателей игрока
        void DrawState(Car obj);

        void Draw(GameObject gameObject, Color color, int textureId);

        void DrawRectangle(Vector2 Position, Vector2 Size, Color Color);

        int LoadSprite(string filePath, out float height, out float width);

        void DrawCircle(float x, float y, float radius, Color Color);
    }
}
