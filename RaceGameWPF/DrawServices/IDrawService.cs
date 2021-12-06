using RaceGame.Api.Common.GameObjects;

namespace RaceGame.Wpf.Client.DrawServices
{
    public interface IDrawService
    {
        void Draw(GameObject gameObject);

        GameObject LoadSprite(string filePath, GameObject obj);
    }
}
