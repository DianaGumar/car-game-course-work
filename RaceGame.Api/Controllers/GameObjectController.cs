using Microsoft.AspNetCore.Mvc;
using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Services.GameService;
using System.Collections.Generic;

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
    }
}
