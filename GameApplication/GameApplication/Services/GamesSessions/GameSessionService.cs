using System;
using System.Collections.Generic;
using GameApplication.Models.Games;
using GameApplication.Models.Games.Battleship;

namespace GameApplication.Services.GamesSessions
{
    public class GameSessionService
    {
        private readonly Dictionary<string, List<IGameSession>> _sessions;

        public GameSessionService()
        {
            _sessions = new Dictionary<string, List<IGameSession>>();
        }

        public void AddSession(string gameName, IGameSession gameSession)
        {
            lock (this)
            {
                if (!_sessions.ContainsKey(gameName))
                {
                    _sessions[gameName] = new List<IGameSession>();
                }
            }
            _sessions[gameName].Add(gameSession);
        }

        public IGameSession GetSession(string gameName, long id, Player loggedPlayer)
        {
            var gameSession = _sessions[gameName].Find(session => id == session.getId());
            if (!gameSession.GetPlayers().Contains(loggedPlayer))
            {
                throw new UnauthorizedAccessException("You are not allowed to join this game");
            }
            return gameSession;
        }

    }
}