using Microsoft.AspNetCore.Mvc;
//using RaceGame.Api.Services.CarService;
//using RaceGame.Api.Services.MoveService;
using RaceGame.Api.Services.GameService;


namespace RaceGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameObjectController : ControllerBase
    {
        private readonly IGameService _gameService;
        //private readonly ICarService _carService;
        //private readonly IMoveService _moveService;

        //public GameObjectController(IGameService gameService, ICarService carService, IMoveService moveService)
        //{
        //    _carService = carService;
        //    _gameService = gameService;
        //    _moveService = moveService;
        //}

        public GameObjectController(IGameService gameService)
        {
            _gameService = gameService;
        }

        //[HttpPost]
        //public IActionResult CreateGameObject(GameObjectType gameObjectType)
        //{
        //    var gameObject = _gameService.CreateGameObject(gameObjectType);
        //    return Ok(gameObject);
        //}

        // POST api/<GameObjectController>
        [HttpPost]
        [Route("ConnectGamer")]
        public IActionResult ConnectGamer([FromQuery] string clientId)
        {  
            var resultCar = _gameService.AddGamer(clientId);
            // получаем состояние мира и также возвращаем и его

            return Ok(resultCar);
        }

        // GET api/<GameObjectController>/5
        [HttpPut]
        [Route("MoveGamer")]
        public IActionResult MoveGamer([FromQuery] string clientId,
            [FromQuery] string gameObjectId, [FromQuery] int direction)
        {
            var resultCar = _gameService.MoveGamer(clientId, gameObjectId, direction);
            //var gameState = _gameService.GetGameState();
            //var result = new { gameObject = gameObject, gameState = gameState };
            return Ok(resultCar);
        }
    }
}
