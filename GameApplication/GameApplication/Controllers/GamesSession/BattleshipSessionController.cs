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
            var loggedUser = new Player("mockedUser-lobbyOwner"); //TODO : get logged user
            var battleshipSession = _sessionService.CreateNewSession(lobbyId, loggedUser);
            return View(battleshipSession);
        }

        public IActionResult JoinGame(int lobbyId)
        {
            var loggedUser = new Player("mockedUser-player"); //TODO : get logged user
            var battleshipSession = _sessionService.FindById(lobbyId, loggedUser);
            return View("StartGame", battleshipSession);
        }
    }
}
