using Microsoft.AspNetCore.Mvc;
using RaceGame.Api.Common.GameObjects;
using RaceGame.Api.Services.GameService;
using RaceGame.Api.Services.LevelService;
using RaceGame.Api.Services.PrizeService;
using RaceGame.Common.Common;
using System.Collections.Generic;
using System.Linq;

namespace RaceGame.Api.Controllers
{
    [Route("api/game-object")]
    [ApiController]
    public class GameObjectController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ILevelService _levelService;
        private readonly IPrizeService _priseService;

        public GameObjectController(IGameService gameService, ILevelService levelService,
            IPrizeService priseService)
        {
            _gameService = gameService;
            _levelService = levelService;
            _priseService = priseService;
        }

        [HttpGet("all")]
        public List<GameObject> GetAllGameObjects()
        {
            return _gameService.GetAllObjects();
        }

        [HttpGet("prizes")]
        public GameObject[] GetPrizes()
        {
            return _priseService.GetGamePrizes();
        }

        [HttpGet("prizes/state")]
        public Point[] GetPrizesState()
        {
            return _priseService.GetPrizesState();
        }

        [HttpGet("level")]
        public List<GameObject> GetLevel()
        {
            return _levelService.GetLevel().ToList();
        }
    }
}
