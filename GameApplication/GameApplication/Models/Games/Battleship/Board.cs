using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public BattleShipBoardStatus ValidateBoard(int[][] board)
        {
            if (board.Any(row => row.Any(val => val != EMPTY_NOT_HIT && val != SHIP_NOT_HIT)))
                return BattleShipBoardStatus.WrongValues;
            //else if (board.Sum(row => row.Sum()) > 20)
            //    return BattleShipBoardStatus.TooManyShips;
            //else if (board.Sum(row => row.Sum()) < 20)
            //    return BattleShipBoardStatus.TooFewShips;
            // else if trudna walidacja stykania sie
            return BattleShipBoardStatus.BoardOK;
        }

        public BattleShipBoardStatus SetBoard(int[][] board)
        {
            var boardstatus = ValidateBoard(board);
            if(boardstatus == BattleShipBoardStatus.BoardOK)
                values = board.Select(x => x.ToArray()).ToArray();
            return boardstatus;
        }

        public int Cannonry(int x, int y)
        {
            switch (values[x][y])
            {
                case EMPTY_NOT_HIT:
                    values[x][y] = EMPTY_HIT;
                    return 0;
                case EMPTY_HIT:
                    return -1;
                case SHIP_NOT_HIT:
                    values[x][y] = SHIP_DESTROYED;
                    return 1;
                case SHIP_DESTROYED:
                    return -1;
                default:
                    return -1;
            }
        }
    }
}