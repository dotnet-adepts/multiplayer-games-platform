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
        private readonly LobbyService _lobbyService;
        private readonly Dictionary<long, BattleshipSession> _sessions;
        private const string GameName = Game.Names.Battleship;

        public BattleshipSessionService(LobbyService lobbyService)
        {
            _lobbyService = lobbyService;
            _sessions = new Dictionary<long, BattleshipSession>();
        }

        public BattleshipSession CreateNewSession(long lobbyId, Player loggedUser)
        {

            var lobby = _lobbyService.FindByIdAndGameName(lobbyId, GameName);
            if (lobby.Owner != loggedUser)
            {
                throw new UnauthorizedAccessException("You have to be an owner of lobby to start game");
            }

            if (_sessions.ContainsKey(lobbyId))
            {
                throw new ArgumentException("Session wih id " + lobbyId + " already exists");
            }

            var connectedPlayers = lobby.ConnectedPlayers;
            var session = new BattleshipSession(lobbyId, connectedPlayers[0], connectedPlayers[1]);
            _sessions.Add(lobbyId, session);
            return session;
        }

        public BattleshipSession FindById(int gameSessionId, Player loggedUser)
        {
            var session = _sessions[gameSessionId];
            if (session.PlayerOne != loggedUser && session.PlayerTwo != loggedUser)
            {
                throw new UnauthorizedAccessException("You are not allowed to join this game");
            }
            return session;
        }
    }
}