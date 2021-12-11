using RaceGame.Api.Common.GameObjects;
using System.Drawing;

namespace RaceGame.Wpf.Client.DrawServices
{
    public interface IDrawService
    {
        void Draw(GameObject gameObject, Color color);

        GameObject LoadSprite(string filePath, GameObject obj);
    }
}
