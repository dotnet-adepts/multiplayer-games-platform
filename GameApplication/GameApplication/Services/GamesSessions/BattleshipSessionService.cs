using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using GameApplication.Models.Games;
using GameApplication.Models.Games.Battleship;
using Microsoft.AspNetCore.Identity;

namespace GameApplication.Services.GamesSessions
{
    public class BattleshipSessionService
    {
        private const string GameName = Game.Names.Battleship;
        private readonly GameSessionService _gameSessionService;

        public BattleshipSessionService(GameSessionService gameSessionService)
        {
            _gameSessionService = gameSessionService;
        }


        public BattleshipSession FindById(int gameSessionId, Player loggedUser)
        {
            return (BattleshipSession) _gameSessionService.GetSession(GameName, gameSessionId, loggedUser);
        }
    }
}