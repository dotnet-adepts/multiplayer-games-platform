using System.Collections.Generic;
using GameApplication.Models.Games;
using GameApplication.Models.Games.Battleship;

namespace GameApplication.Factories
{
    public class BatlleshipGameSessionFactory : IGameSessionFactory
    {
        public IGameSession Create(long lobbyId, List<Player> players)
        {
            return new BattleshipSession(lobbyId, players[0], players[1]);
        }
    }
}