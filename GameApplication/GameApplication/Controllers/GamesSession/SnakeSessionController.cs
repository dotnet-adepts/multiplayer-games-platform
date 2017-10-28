using System;
using Microsoft.AspNetCore.Mvc;

namespace GameApplication.Controllers.GamesSession
{
    public class SnakeSessionController : Controller, IGameSessionController
    {

        public IActionResult StartGame(int lobbyId)
        {   
            throw new NotImplementedException();
        }

        public IActionResult JoinGame(int gameSessionId)
        {
            throw new NotImplementedException();
        }
    }
}
