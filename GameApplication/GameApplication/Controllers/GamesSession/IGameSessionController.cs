using Microsoft.AspNetCore.Mvc;

namespace GameApplication.Controllers.GamesSession
{
    interface IGameSessionController
    {
        IActionResult StartGame(int lobbyId);

        IActionResult JoinGame(int gameSessionId);
    }
}
