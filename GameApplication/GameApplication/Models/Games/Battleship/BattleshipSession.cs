using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace GameApplication.Models.Games.Battleship
{
    public class BattleshipSession
    {
        public long Id;
        public Player PlayerOne;
        public Player PlayerTwo;
        //TODO: game state

        public BattleshipSession(long id, Player playerOne, Player playerTwo)
        {
            Id = id;
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
        }
    }
}