using Microsoft.AspNetCore.Mvc;

namespace GameApplication.Controllers.GamesSession
{
    interface IGameSessionController
    {
        IActionResult JoinGame(int gameSessionId);
    }
}
