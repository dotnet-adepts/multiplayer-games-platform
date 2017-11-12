using GameApplication.Models.Games;
using GameApplication.Services.GamesSessions;
using Microsoft.AspNetCore.Mvc;

namespace GameApplication.Controllers.GamesSession
{
    public class BattleshipSessionController : Controller, IGameSessionController
    {

        private readonly BattleshipSessionService _sessionService;

        public BattleshipSessionController(BattleshipSessionService sessionService)
        {
            _sessionService = sessionService;
        }


        public IActionResult StartGame(int lobbyId)
        {
            var battleshipSession = _sessionService.CreateNewSession(lobbyId, new Player(User));
            return View(battleshipSession);
        }

        public IActionResult JoinGame(int lobbyId)
        {
            var battleshipSession = _sessionService.FindById(lobbyId, new Player(User));
            return View("StartGame", battleshipSession);
        }
    }
}
