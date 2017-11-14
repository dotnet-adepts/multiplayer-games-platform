using System.Collections.Generic;

namespace GameApplication.Models.Games.Battleship
{
    public interface IGameSession
    {
        long getId();
        List<Player> GetPlayers();
        string GetJoinUrl();

    }
}