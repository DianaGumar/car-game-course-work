using Microsoft.AspNetCore.Mvc;
using RaceGame.Api.Common.GameObjects;
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

        [HttpPut("{clientId}/shot")]
        public IActionResult PutGetShot(string clientId)
        {
            _carServise.GetShot(clientId);

            return Ok();
        }

        [HttpGet("bullets")]
        public Bullet[] GetBullets()
        {
            return _carServise.GetBullets();
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

        // update-gamer/move
        [HttpPut("texture")]
        public IActionResult PutTexture([FromBody] Car car)
        {
            _carServise.UpdateCarTexture(car);

            return Ok();
        } 

        [HttpDelete("{clientId}")]
        public void Delete(string clientId)
        {
            _carServise.DeleteCar(clientId);
        }


    }
}
