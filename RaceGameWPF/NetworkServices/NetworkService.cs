using Newtonsoft.Json;
using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Common.GameObjects.Car;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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

        public List<GameObject> GetGameObjects(string gamerId)
        {
            var response = _httpClient.GetAsync($"api/game-object/{gamerId}/all").Result;
            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<List<GameObject>>(content);

            return result;
        }

        public Car CreateGamer(string clientId)
        {
            var response = _httpClient.PostAsJsonAsync<string>("api/gamer", clientId).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            Car result = JsonConvert.DeserializeObject<Car>(content);

            return result;
        }

        public Car MoveGamer(string gamerId, int direction)
        {
            var response = _httpClient
                .PutAsJsonAsync<int>($"api/gamer/{gamerId}/move", direction).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            Car result = JsonConvert.DeserializeObject<Car>(content);

            return result;
        }
    }
}
