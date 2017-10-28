using GameApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameApplication.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameService _gameService;

        public GamesController(GameService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            var allGames = _gameService.FindAll();
            return View(allGames);
        }
    }
}