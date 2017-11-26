using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameApplication.Models.Games.Battleship
{
    public enum BattleShipBoardStatus
    {
        BoardOK = 1,
        WrongValues = -1,
        TooManyShips = -2,
        TooFewShips = -3,
        ErrorsInPlacement = -4
    };
}
