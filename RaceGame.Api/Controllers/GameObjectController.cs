using Microsoft.AspNetCore.Mvc;
using RaceGame.Api.Common.GameObjects;
//using RaceGame.Api.Services.CarService;
//using RaceGame.Api.Services.MoveService;
using RaceGame.Api.Services.GameService;
using System.Collections.Generic;

namespace RaceGame.Api.Controllers
{
    [Route("api/game-object")]
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

        [HttpGet]
        public List<GameObject> GetAll()
        {
            return _gameService.GetGameObjects();
        }
    }
}
