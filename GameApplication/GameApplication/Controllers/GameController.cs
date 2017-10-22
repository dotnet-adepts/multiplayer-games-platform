using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameApplication.Models;
using GameApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameApplication.Controllers
{

    [Route("/games")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public List<Game> FindAll()
        {
            return _gameService.FindAll();
        }

        [HttpGet("{id}", Name = "GetGame")]
        public IActionResult FindById(long id)
        {
            var game = _gameService.FindById(id);
            if (game == null)
            {
                return NotFound();
            }
            return new ObjectResult(game);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] Game game)
        {
            if (game == null)
            {
                return BadRequest();
            }
            _gameService.Save(game);
            return CreatedAtRoute("GetGame", new { id = game.GameId }, game);
        }
    }
}