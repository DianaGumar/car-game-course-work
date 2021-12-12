using Microsoft.AspNetCore.Mvc;
using RaceGame.Api.Common.GameObjects.Car;
using RaceGame.Api.Services.CarService;
using RaceGame.Api.Services.GameService;
using System.Collections.Generic;

namespace RaceGame.Api.Controllers
{
    [Route("api/gamer")]
    [ApiController]
    public class GamerController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ICarService _carServise;

        public GamerController(IGameService gameService, ICarService carServise)
        {
            _gameService = gameService;
            _carServise = carServise;
        }

        // POST connect gamer
        [HttpPost]
        public Car Post([FromBody] string clientId)
        {
            var resultCar = _gameService.AddGamer(clientId);

            return resultCar;
        }

        // get enemy gamer
        [HttpGet("{clientId}/enemy")]
        public Car Get(string clientId)
        {
            return _carServise.GetEnemyCar(clientId);
        }

        [HttpGet]
        public List<Car> Get()
        {
            return _carServise.GetCars();
        }

        // update-gamer/move
        [HttpPut("{clientId}/move/{direction}")]
        public Car PutMove(string clientId, int direction)
        {
            var resultCar = _carServise.MoveGamer(clientId, direction);

            return resultCar;
        }

        [HttpDelete("{clientId}")]
        public void Delete(string clientId)
        {
            _carServise.DeleteCar(clientId);
        }
    }
}
