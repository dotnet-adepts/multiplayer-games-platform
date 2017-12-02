using System.Linq;

namespace GameApplication.Models.Games.Battleship
{
    public class Board
    {
        #region Private Const Values

        private const int BOARD_SIZE = 10;

        private const int EMPTY_NOT_HIT = 0;
        private const int SHIP_NOT_HIT = 1;
        private const int EMPTY_HIT = 2;
        private const int SHIP_DESTROYED = 3;

        #endregion

        private int[][] values;

        public Board()
        {
            values = new int[BOARD_SIZE][];
            for (int i = 0; i < BOARD_SIZE; i++)
                values[i] = new int[BOARD_SIZE];
        }

        public int[][] GetBoard()
        {
            return values;
        }

        public BattleshipBoardStatus ValidateBoard(int[][] board)
        {
            if (board.Any(row => row.Any(val => val != EMPTY_NOT_HIT && val != SHIP_NOT_HIT)))
                return BattleshipBoardStatus.WrongValues;
            //else if (board.Sum(row => row.Sum()) > 20)
            //    return BattleShipBoardStatus.TooManyShips;
            //else if (board.Sum(row => row.Sum()) < 20)
            //    return BattleShipBoardStatus.TooFewShips;
            // else if trudna walidacja stykania sie
            return BattleshipBoardStatus.BoardOK;
        }

        public BattleshipBoardStatus SetBoard(int[][] board)
        {
            var boardstatus = ValidateBoard(board);
            if(boardstatus == BattleshipBoardStatus.BoardOK)
                values = board.Select(x => x.ToArray()).ToArray();
            return boardstatus;
        }

        public BattleshipMoveStatus Cannonry(int x, int y)
        {
            switch (values[x][y])
            {
                case EMPTY_NOT_HIT:
                    values[x][y] = EMPTY_HIT;
                    return BattleshipMoveStatus.ShipMiss;
                case EMPTY_HIT:
                    return BattleshipMoveStatus.WrongMove;
                case SHIP_NOT_HIT:
                    values[x][y] = SHIP_DESTROYED;
                    if(IsGameOver())
                        return BattleshipMoveStatus.GameOver;
                    else
                        return BattleshipMoveStatus.ShipDown;
                case SHIP_DESTROYED:
                default:
                    return BattleshipMoveStatus.WrongMove;
            }
        }

        private bool IsGameOver()
        {
            return !values.Any(row => row.Any(val => val == SHIP_NOT_HIT));
        }
    }
}