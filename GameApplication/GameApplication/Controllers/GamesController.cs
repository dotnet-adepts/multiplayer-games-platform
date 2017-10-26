using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameApplication.Models.Games;
using GameApplication.Services;

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

        public string Lobby(string game)
        {
            return "wybrana gra: " + game;
        }

    }
}
