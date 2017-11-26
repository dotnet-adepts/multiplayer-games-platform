using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameApplication.Models.Games.Battleship
{
    public class BattleshipPlayer : Player
    {
        public Board Board { get; set; }
        public bool Ready { get; set; }

        public BattleshipPlayer(ClaimsPrincipal contextUser) : base(contextUser)
        {
            Board = new Board();
            Ready = false;
        }

        public void SetToReady()
        {
            Ready = true;
        }

        public Player GetAsPlayer()
        {
            return new Player(this.Principal);
        }

    }
}
