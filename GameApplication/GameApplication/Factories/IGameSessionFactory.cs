using System.Collections.Generic;
using GameApplication.Models.Games;
using GameApplication.Models.Games.Battleship;

namespace GameApplication.Factories
{
    public interface IGameSessionFactory
    {
        IGameSession Create(long lobbyId, List<Player> players);
    }
}