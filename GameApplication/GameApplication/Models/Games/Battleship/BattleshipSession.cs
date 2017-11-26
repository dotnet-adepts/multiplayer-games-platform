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
        public BattleshipPlayer PlayerOne;
        public BattleshipPlayer PlayerTwo;
        //TODO: game state
        public int Example = 0;

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

        public BattleShipBoardStatus SetBoard(BattleshipPlayer player, int[][] board)
        {
            return GetCurrentPlayer(player).Board.SetBoard(board);
        }

        public void SetPlayerReady(BattleshipPlayer player)
        {
            GetCurrentPlayer(player).SetToReady();
        }

        public Board GetMyBoard(BattleshipPlayer player)
        {
            if (player == PlayerOne)
                return PlayerOne.Board;
            else
                return PlayerTwo.Board;
        }

        public BattleshipPlayer GetCurrentPlayer(BattleshipPlayer player)
        {
            if (PlayerOne == player)
                return PlayerOne;
            else
                return PlayerTwo;
        }

        public bool ReadyToStart()
        {
            return PlayerOne.Ready && PlayerTwo.Ready;
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