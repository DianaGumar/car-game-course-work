using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RaceGame.Wpf.Client.NetworkServices
{
    public interface INetworkService
    {
        public Task<List<GameObject>> GetGameObjects();

        public Task<Car> CreateGamer(string clientId);

        public Task<Car> MoveGamer(string gamerId, int direction);
    }
}
