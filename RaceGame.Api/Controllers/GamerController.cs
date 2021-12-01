using Microsoft.AspNetCore.Mvc;
using RaceGame.Api.Common.GameObjects.Car;
using RaceGame.Api.Services.GameService;

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
            // получаем состояние мира и также возвращаем и его

            return resultCar;
        }

        // update-gamer/move
        // PUT api/<GamerController>/5
        [HttpPut("{clientId}/move")]
        public Car PutMove(string clientId, [FromBody] int direction)
        {
            var resultCar = _gameService.MoveGamer(clientId, direction);
            //var gameState = _gameService.GetGameState();
            //var result = new { gameObject = gameObject, gameState = gameState };
            return resultCar;
        }

        //// update-gamer/move
        //// PUT api/<GamerController>/5
        //[HttpPut("{clientId}/prize")]
        //public Car PutPrize(string clientId, [FromQuery] int direction)
        //{
        //    var resultCar = _gameService.MoveGamer(clientId, gameObjectId, direction);
        //    //var gameState = _gameService.GetGameState();
        //    //var result = new { gameObject = gameObject, gameState = gameState };
        //    return resultCar;
        //}

        // DELETE api/<GamerController>/5
        [HttpDelete("{clientId}")]
        public void Delete(string clientId)
        {
            _gameService.DeleteGamer(clientId);
        }
    }
}
