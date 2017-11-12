using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using GameApplication.Exceptions;
using GameApplication.Models.Games;
using GameApplication.Services;
using Microsoft.AspNetCore.SignalR;

namespace GameApplication.Hubs
{
    public class LobbyHub : Hub
    {
        private readonly LobbyService _lobbyService;

        public LobbyHub(LobbyService lobbyService)
        {
            _lobbyService = lobbyService;
        }

        public async Task JoinLobby(long lobbyId, string gameName)
        {
            var player = GetLoggedPlayer();
            var groupName = lobbyId.ToString();
            var lobby = _lobbyService.FindByIdAndGameName(lobbyId, gameName);
            try
            {
                lobby.AddPlayer(player);
                Context.Connection.Metadata.Add("lobbyId", lobbyId);
                Context.Connection.Metadata.Add("gameName", gameName);
                await Groups.AddAsync(Context.ConnectionId, groupName);
                await Clients.Group(groupName).InvokeAsync("updatePlayers", converPlayerListToNameList(lobby.ConnectedPlayers));
            }
            catch (FullLobbyExceptioncs e)
            {
                await Clients.Client(Context.ConnectionId).InvokeAsync("handleFullLobby");
                await Clients.Client(Context.ConnectionId).InvokeAsync("updatePlayers", converPlayerListToNameList(lobby.ConnectedPlayers));
            }
        }


        public async Task StartGame(long lobbyId)
        {
            await Clients.Group(lobbyId.ToString()).InvokeAsync("startGame");
        }


        public override Task OnDisconnectedAsync(Exception exception) //TODO : remove lobby from service when empty
        {
            var lobbyId = (long) Context.Connection.Metadata["lobbyId"];
            var gameName = (string) Context.Connection.Metadata["gameName"];

            var lobby = _lobbyService.FindByIdAndGameName(lobbyId, gameName);
            lobby.RemovePlayer(new Player(Context.User));

            Clients.Group(lobbyId.ToString()).InvokeAsync("updatePlayers", converPlayerListToNameList(lobby.ConnectedPlayers));
            return base.OnDisconnectedAsync(exception);
        }


        public Player GetLoggedPlayer()
        {
            return new Player(Context.User);
        }

        public List<string> converPlayerListToNameList(List<Player> players)
        {
            var list = new List<string>();
            foreach (Player player in players)
            {
                list.Add(player.GetName());
            }
            return list;
        }
    }
}