using Microsoft.AspNetCore.Mvc;
using RaceGame.Api.Common.GameObjects.Car;
using RaceGame.Api.Services.GameService;
using System.Collections.Generic;
using System.Linq;

namespace RaceGame.Api.Controllers
{
    [Route("api/gamer")]
    [ApiController]
    public class GamerController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamerController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // POST connect gamer
        [HttpPost]
        public Car Post([FromBody] string clientId)
        {
            var resultCar = _gameService.AddGamer(clientId);
            // получаем состояние мира и также возвращаем и его (это уже не rest)

            return resultCar;
        }

        // get enemy gamer
        [HttpGet("{clientId}/enemy")]
        public Car Get(string clientId)
        {
            return _gameService.GetAllGamers().FirstOrDefault(c => !c.Id.Equals(clientId));
        }

        // get enemy gamer
        [HttpGet]
        public List<Car> Get()
        {
            return _gameService.GetAllGamers();
        }

        // update-gamer/move
        // PUT api/<GamerController>/5
        [HttpPut("{clientId}/move/{direction}")]
        public Car PutMove(string clientId, int direction)
        {
            var resultCar = _gameService.MoveGamer(clientId, direction);
            //var gameState = _gameService.GetGameState();
            //var result = new { gameObject = gameObject, gameState = gameState };
            return resultCar;
        }

        // DELETE api/<GamerController>/5
        [HttpDelete("{clientId}")]
        public void Delete(string clientId)
        {
            _gameService.DeleteGamer(clientId);
        }
    }
}
