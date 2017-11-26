using System;
using System.Threading.Tasks;
using GameApplication.Exceptions;
using GameApplication.Models.Games;
using GameApplication.Services.GamesSessions;
using Microsoft.AspNetCore.SignalR;
using GameApplication.Models.Games.Battleship;
using BoardStatus = GameApplication.Models.Games.Battleship.BattleShipBoardStatus;

namespace GameApplication.Hubs
{
    public class BattleshipHub : Hub
    {
        private readonly BattleshipSessionService _battleshipSessionService;

        public BattleshipHub(BattleshipSessionService battleshipSessionService)
        {
            _battleshipSessionService = battleshipSessionService;
        }


        public async Task JoinGame(int sessionId)
        {
            var loggedPlayer = GetLoggedPlayer();
            var groupName = MapSessionIdToGroupName(sessionId);
            await Groups.AddAsync(Context.ConnectionId, groupName);
        }

        public async Task UpdateExample(int sessionId)
        {
            var loggedPlayer = GetLoggedPlayer();
            var battleshipSession = _battleshipSessionService.FindById(sessionId, loggedPlayer);
            battleshipSession.UpdateExample();

            var groupName = MapSessionIdToGroupName(sessionId);
            await Clients.Group(groupName).InvokeAsync("updateExampleValueInView", battleshipSession.Example);
        }

        public async Task SetBoard(int sessionId, int[][] board)
        {
            var loggedPlayer = GetLoggedPlayer();
            var battleshipSession = _battleshipSessionService.FindById(sessionId, loggedPlayer.GetAsPlayer());
            switch (battleshipSession.SetBoard(loggedPlayer, board))
            {
                case BoardStatus.BoardOK:
                    battleshipSession.SetPlayerReady(loggedPlayer);
                    if (battleshipSession.ReadyToStart())
                    {
                        var groupName = MapSessionIdToGroupName(sessionId);
                        await Clients.Group(groupName).InvokeAsync("playersReady");
                    }
                    else
                        await Clients.Client(Context.ConnectionId).InvokeAsync("waitForOpponent");
                    break;
                case BoardStatus.TooManyShips:
                    await Clients.Client(Context.ConnectionId).InvokeAsync("tooManyShips", true);
                    break;
                case BoardStatus.TooFewShips:
                    await Clients.Client(Context.ConnectionId).InvokeAsync("tooManyShips", false);
                    break;
                case BoardStatus.ErrorsInPlacement:
                    await Clients.Client(Context.ConnectionId).InvokeAsync("errorsInShipPlacement");
                    break;
                case BoardStatus.WrongValues:
                    await Clients.Client(Context.ConnectionId).InvokeAsync("badInput");
                    break;
            }
        }

        public string MapSessionIdToGroupName(int sessionId)
        {
            return "group-" + sessionId;
        }

        public BattleshipPlayer GetLoggedPlayer()
        {
            return new BattleshipPlayer(Context.User);
        }

    }
}