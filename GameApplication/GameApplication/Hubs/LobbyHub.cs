using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using GameApplication.Exceptions;
using GameApplication.Models.Games;
using GameApplication.Services;
using GameApplication.Services.GamesSessions;
using Microsoft.AspNetCore.SignalR;

namespace GameApplication.Hubs
{
    public class LobbyHub : Hub
    {
        private readonly LobbyService _lobbyService;
        private readonly GameSessionService _gameSessionService;


        public LobbyHub(LobbyService lobbyService, GameSessionService gameSessionService)
        {
            _lobbyService = lobbyService;
            _gameSessionService = gameSessionService;
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
                await Clients.Group(groupName).InvokeAsync("updatePlayers", ConvertPlayersToNames(lobby.ConnectedPlayers));
            }
            catch (FullLobbyExceptioncs e)
            {
                await Clients.Client(Context.ConnectionId).InvokeAsync("handleFullLobby");
                await Clients.Client(Context.ConnectionId).InvokeAsync("updatePlayers", ConvertPlayersToNames(lobby.ConnectedPlayers));
            }
        }


        public async Task StartGame(long lobbyId, string gameName)
        {
            var loggedPlayer = GetLoggedPlayer();
            var lobby = _lobbyService.FindByIdAndGameName(lobbyId, gameName);
            if (lobby.Owner != loggedPlayer)
            {
                throw new UnauthorizedAccessException("You have to be an owner of lobby to start game");
            }
            var players = lobby.ConnectedPlayers;
            var gameSession = lobby.StartGameSession();
            _gameSessionService.AddSession(gameName, gameSession);
            await Clients.Group(lobbyId.ToString()).InvokeAsync("startGame", gameSession.GetJoinUrl());
        }


        public override Task OnDisconnectedAsync(Exception exception) //TODO : remove lobby when empty
        {
            var lobbyId = (long) Context.Connection.Metadata["lobbyId"];
            var gameName = (string) Context.Connection.Metadata["gameName"];

            var lobby = _lobbyService.FindByIdAndGameName(lobbyId, gameName);
            lobby.RemovePlayer(new Player(Context.User));
            if (lobby.ConnectedPlayers.Count == 0)
            {
                _lobbyService.Remove(gameName, lobby);
            }
            Clients.Group(lobbyId.ToString()).InvokeAsync("updatePlayers", ConvertPlayersToNames(lobby.ConnectedPlayers));
            return base.OnDisconnectedAsync(exception);
        }


        public Player GetLoggedPlayer()
        {
            return new Player(Context.User);
        }

        public List<string> ConvertPlayersToNames(List<Player> players)
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