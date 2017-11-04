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