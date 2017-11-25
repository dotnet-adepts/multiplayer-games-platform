using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;

namespace GameApplication.Models.Games.Battleship
{
    public class BattleshipSession : IGameSession
    {
        public long Id;
        public Player PlayerOne;
        public Player PlayerTwo;
        //TODO: game state
        public int Example = 0;

        public BattleshipSession(long id, Player playerOne, Player playerTwo)
        {
            Id = id;
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
        }

        public long getId()
        {
            return Id;
        }

        public List<Player> GetPlayers()
        {
            return new List<Player>() { PlayerOne, PlayerTwo };
        }

        public string GetJoinUrl()
        {
            return "/BattleshipSession/JoinGame?lobbyId=" + Id;
        }

        public void UpdateExample()
        {
            Example++;
        }
    }
}