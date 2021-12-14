using RaceGame.Api.Common.GameObjects.Car;
using System.Windows.Input;

namespace RaceGame.Wpf.Client.ClientState
{
    public interface IClientStateService
    {
        void GetGameObjects();
        bool ConnectClient();
        void ClientAction(Key key);
        void Update();
        void Draw();
        void EndGame();
        bool IsWon();
        bool IsYouWon();
        void ResetGame();
    }
}
