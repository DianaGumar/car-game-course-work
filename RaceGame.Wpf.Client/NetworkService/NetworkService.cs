using Newtonsoft.Json;
using RaceGame.Api.Common.GameObjects.Car;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RaceGame.Wpf.Client.NetworkService
{
    public class NetworkService
    {
        private HttpClient _httpClient;
        private Car gamer;

        public NetworkService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:5001/");

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Car> ConnectGamer()
        {
            var clientId = Guid.NewGuid().ToString();

            var response = await _httpClient.PostAsJsonAsync<string>("api/GameObject/ConnectGamer", clientId);
            var contents = await response.Content.ReadAsStringAsync();

            Car result = JsonConvert.DeserializeObject<Car>(contents);

            return result;
        }

        public async Task<Car> MoveGamer(Car obj)
        {
            var response = await _httpClient.
                PostAsJsonAsync($"api/GameObject/MoveGamer?clientId={obj}&gameObjectId={obj}&direction={obj}", "");
            var contents = await response.Content.ReadAsStringAsync();

            Car result = JsonConvert.DeserializeObject<Car>(contents);

            return result;
        }
    }
}
