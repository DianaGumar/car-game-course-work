using Newtonsoft.Json;
using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RaceGame.Wpf.Client.NetworkServices
{
    public class NetworkService : INetworkService
    {
        private HttpClient _httpClient;

        public NetworkService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:5001/");

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<GameObject>> GetGameObjects()
        {
            var response = await _httpClient.GetAsync("api/game-object");
            var contents = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<GameObject>>(contents);

            return result;
        }

        public async Task<Car> CreateGamer(string clientId)
        {
            var response = await _httpClient.PostAsJsonAsync<string>("api/gamer", clientId);
            var contents = await response.Content.ReadAsStringAsync();

            Car result = JsonConvert.DeserializeObject<Car>(contents);

            return result;
        }

        public async Task<Car> MoveGamer(string gamerId, int direction)
        {
            var response = await _httpClient
                .PostAsJsonAsync<int>($"api/gamer/{gamerId}/move", direction);
            var contents = await response.Content.ReadAsStringAsync();

            Car result = JsonConvert.DeserializeObject<Car>(contents);

            return result;
        }
    }
}
