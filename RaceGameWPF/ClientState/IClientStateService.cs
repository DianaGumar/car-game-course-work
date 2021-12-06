using System.Windows.Input;

namespace RaceGame.Wpf.Client.ClientState
{
    public interface IClientStateService
    {
        void ConnectClient();
        void ClientAction(Key key);
        void Update();
        void Draw();
    }
}
