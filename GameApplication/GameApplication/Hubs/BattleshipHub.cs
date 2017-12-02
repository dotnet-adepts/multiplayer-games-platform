using GameApplication.Models.Games.Battleship;
using GameApplication.Services.GamesSessions;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using BoardStatus = GameApplication.Models.Games.Battleship.BattleshipBoardStatus;
using MoveStatus = GameApplication.Models.Games.Battleship.BattleshipMoveStatus;

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
                        await Clients.Group(groupName).InvokeAsync("playersReady", battleshipSession.GetGameUrl());
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

        public async Task GetBoard(int sessionId)
        {
            var loggedPlayer = GetLoggedPlayer();
            var battleshipSession = _battleshipSessionService.FindById(sessionId, loggedPlayer.GetAsPlayer());
            var board = battleshipSession.GetPlayerBoard(loggedPlayer);
            await Clients.Client(Context.ConnectionId).InvokeAsync("playerBoard", board.GetBoard());
        }

        public async Task IsItMyTurn(int sessionId)
        {
            var loggedPlayer = GetLoggedPlayer();
            var battleshipSession = _battleshipSessionService.FindById(sessionId, loggedPlayer.GetAsPlayer());

            if(battleshipSession.IsPlayerMove(loggedPlayer))
                await Clients.Client(Context.ConnectionId).InvokeAsync("yourTurn");
            else
                await Clients.Client(Context.ConnectionId).InvokeAsync("waitForOpponent");
        }

        public async Task Move(int sessionId, int x, int y)
        {
            var loggedPlayer = GetLoggedPlayer();
            var battleshipSession = _battleshipSessionService.FindById(sessionId, loggedPlayer.GetAsPlayer());
            if (battleshipSession.IsPlayerMove(loggedPlayer))
            {
                var groupName = MapSessionIdToGroupName(sessionId);
                switch (battleshipSession.Move(loggedPlayer, x, y))
                {  
                    case MoveStatus.ShipDown:
                        await Clients.Group(groupName).InvokeAsync("shipDown", x, y);
                        break;
                    case MoveStatus.ShipMiss:
                        await Clients.Group(groupName).InvokeAsync("shipMiss", x, y);
                        break;
                    case MoveStatus.GameOver:
                        await Clients.Group(groupName).InvokeAsync("gameOver", x, y);
                        break;
                }
            }
            else
            {
                await Clients.Client(Context.ConnectionId).InvokeAsync("notYourTurn");
            }
        }

        private string MapSessionIdToGroupName(int sessionId)
        {
            return "group-" + sessionId;
        }

        private BattleshipPlayer GetLoggedPlayer()
        {
            return new BattleshipPlayer(Context.User);
        }
    }
}