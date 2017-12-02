using System.Security.Claims;

namespace GameApplication.Models.Games.Battleship
{
    public class BattleshipPlayer : Player
    {
        public Board Board { get; set; }

        public Board OpponentBoard { get; set; }
        public bool Ready { get; set; }

        public BattleshipPlayer(ClaimsPrincipal contextUser) : base(contextUser)
        {
            Board = new Board();
            OpponentBoard = new Board();
            Ready = false;
        }

        public void SetToReady()
        {
            Ready = true;
        }

        public void SetToNotReady()
        {
            Ready = false;
        }

        public Player GetAsPlayer()
        {
            return new Player(this.Principal);
        }

        public void ChangeTurn()
        {
            Ready = !Ready;
        }
    }
}
