using System;
using System.Threading.Tasks;
using GameApplication.Exceptions;
using GameApplication.Models.Games;
using GameApplication.Services.GamesSessions;
using Microsoft.AspNetCore.SignalR;

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
            var battleshipSession = _battleshipSessionService.FindById(sessionId, loggedPlayer);
            var myBoard = battleshipSession.GetMyBoard(loggedPlayer);
            myBoard.SetBoard(board);

            var groupName = MapSessionIdToGroupName(sessionId);
            await Clients.Group(groupName).InvokeAsync("playerReady", loggedPlayer.GetName());
        }

        public string MapSessionIdToGroupName(int sessionId)
        {
            return "group-" + sessionId;
        }

        public Player GetLoggedPlayer()
        {
            return new Player(Context.User);
        }

    }
}