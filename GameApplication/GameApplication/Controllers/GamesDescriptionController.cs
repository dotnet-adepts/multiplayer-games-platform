using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameApplication.Models;

namespace GameApplication.Controllers
{
    public class GamesController : Controller
    {
        public IActionResult Index()
        {
            var snake = new GameDescription("Snake", "Steruj wężem, pokonaj innych!", 2, 4, "Zręcznościowe");
            var battleship = new GameDescription("Statki", "Zatop statki przeciwnika zanim on zatopi Twoje!", 2, 2, "Strategiczne");
            var gamesDescriptions = new List<GameDescription> { snake, battleship };

            return View(gamesDescriptions);
        }

        public string Lobby(string game)
        {
            return "wybrana gra: " + game;
        }

    }
}
