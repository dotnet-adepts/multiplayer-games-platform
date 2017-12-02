namespace GameApplication.Models.Games.Battleship
{
    public enum BattleshipBoardStatus
    {
        BoardOK = 1,
        WrongValues = -1,
        TooManyShips = -2,
        TooFewShips = -3,
        ErrorsInPlacement = -4
    };

    public enum BattleshipMoveStatus
    {
        ShipDown = 1,
        ShipMiss = 2,
        GameOver = 3,
        WrongMove = -1
    }
}
