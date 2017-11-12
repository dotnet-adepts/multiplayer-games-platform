using System;
using System.Threading.Tasks;
using GameApplication.Services;
using Microsoft.AspNetCore.SignalR;

namespace GameApplication.Hubs
{
    public class LobbyHub : Hub
    {
        private readonly LobbyService _lobbyService;


        public async Task JoinLobby(long lobbyId, string userToken)
        {
            var identityName = Context.User.Identity.Name;
            string userName = MapTokenToUserName(userToken);
            await Groups.AddAsync(userName, lobbyId.ToString());
        }

        public async Task LeaveLobby(long lobbyId, string userToken)
        {
            string userName = MapTokenToUserName(userToken);
            await Groups.RemoveAsync(userName, lobbyId.ToString());
        }

        public async Task StartGame(long lobbyId, string userToken)
        {
            string userName = MapTokenToUserName(userToken);
            await Clients.Group(lobbyId.ToString()).InvokeAsync("startGame");
        }

        public string MapTokenToUserName(string token)
        {
            return token == "test" ? "user1" : "user2";
        }
    }
}