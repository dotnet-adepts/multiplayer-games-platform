using System;
using System.Collections.Generic;

namespace GameApplication.Models.Games.Battleship
{
    public class BattleshipSession : IGameSession
    {
        public long Id;
        public BattleshipPlayer PlayerOne;
        public BattleshipPlayer PlayerTwo;

        public BattleshipSession(long id, Player playerOne, Player playerTwo)
        {
            Id = id;
            PlayerOne = new BattleshipPlayer(playerOne.Principal);
            PlayerTwo = new BattleshipPlayer(playerTwo.Principal);
        }

        public long getId()
        {
            return Id;
        }

        public List<Player> GetPlayers()
        {
            return new List<Player>() { new Player(PlayerOne.Principal), new Player(PlayerTwo.Principal) };
        }

        public BattleshipBoardStatus SetBoard(BattleshipPlayer player, int[][] board)
        {
            return GetCurrentPlayer(player).Board.SetBoard(board);
        }

        public void SetPlayerReady(BattleshipPlayer player)
        {
            GetCurrentPlayer(player).SetToReady();
        }

        public Board GetPlayerBoard(BattleshipPlayer player)
        {
            return GetCurrentPlayer(player).Board;
        }

        public Board GetOpponentBoard(BattleshipPlayer player)
        {
            return GetCurrentPlayer(player).OpponentBoard;
        }

        public BattleshipPlayer GetCurrentPlayer(BattleshipPlayer player)
        {
            if (PlayerOne == player)
                return PlayerOne;
            else
                return PlayerTwo;
        }

        public BattleshipPlayer GetOppositePlayer(BattleshipPlayer player)
        {
            if (PlayerOne != player)
                return PlayerOne;
            else
                return PlayerTwo;
        }

        public bool ReadyToStart()
        {
            if (PlayerOne.Ready && PlayerTwo.Ready)
            {
                ChoosePlayerToStart();
                return true;
            }
            else
                return false;
        }

        public string GetJoinUrl()
        {
            return "/BattleshipSession/JoinGame?lobbyId=" + Id;
        }

        public string GetGameUrl()
        {
            return "/BattleshipSession/Game?lobbyId=" + Id;
        }

        public bool IsPlayerMove(BattleshipPlayer player)
        {
            return GetCurrentPlayer(player).Ready;
        }

        public BattleshipMoveStatus Move(BattleshipPlayer player, int x, int y)
        {
            var opponentBoard = GetCurrentPlayer(player).OpponentBoard;
            var status = GetOppositePlayer(player).Board.Cannonry(opponentBoard, x, y);
            switch (status)
            {
                case BattleshipMoveStatus.ShipMiss:
                    ChangePlayerTurn();
                    return status;
                case BattleshipMoveStatus.ShipDown:
                case BattleshipMoveStatus.GameOver:
                    return status;
                default:
                    return status;
            }
        }

        private void ChangePlayerTurn()
        {
            PlayerOne.ChangeTurn();
            PlayerTwo.ChangeTurn();
        }

        private void ChoosePlayerToStart()
        {
            PlayerOne.SetToNotReady();
            PlayerTwo.SetToNotReady();

            Random random = new Random();
            if (random.Next() % 2 == 1)
                PlayerOne.ChangeTurn();
            else
                PlayerTwo.ChangeTurn();
        }
    }
}