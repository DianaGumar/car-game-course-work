using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using System.Drawing;

namespace RaceGame.Wpf.Client.DrawServices
{
    public interface IDrawService
    {
        // отрисовка показателей игрока
        void DrawState(Car obj);

        void Draw(GameObject gameObject, Color color);

        GameObject LoadSprite(string filePath, GameObject obj);
    }
}
