namespace RaceGame.Wpf.Client.ClientState
{
    public interface IClientStateService
    {
        public void ConnectClient();
        public void Update();
        public void Draw();
    }
}
