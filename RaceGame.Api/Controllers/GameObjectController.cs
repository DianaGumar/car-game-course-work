using Microsoft.AspNetCore.Mvc;
using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Services.GameService;
using System.Collections.Generic;
using System.Linq;

namespace RaceGame.Api.Controllers
{
    [Route("api/game-object")]
    [ApiController]
    public class GameObjectController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameObjectController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("{gamerId}/all")]
        public List<GameObject> GetAll(string gamerId)
        {
            return _gameService.GetGameObjects(gamerId);
        }

        [HttpGet("all")]
        public List<GameObject> GetAll()
        {
            return _gameService.GetGameObjects();
        }

        [HttpGet("level")]
        public List<GameObject> GetLevel()
        {
            return _gameService.GetLevel().ToList();
        }
    }
}
