using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameApplication.Models.Games;
using GameApplication.Services;

namespace GameApplication.Controllers
{
    public class LobbyController: Controller
    {

        private readonly LobbyService _lobbyService;

        public LobbyController(LobbyService lobbyService)
        {
            this._lobbyService = lobbyService;
        }

        public IActionResult Index(string gameName)
        {
            ViewData["gameName"] = gameName;
            var lobbies = _lobbyService.FindAllByGameName(gameName);
            return View(lobbies);
        }

        public IActionResult Create(string gameName)
        {
            var mockedPlayer = new Player("mockedUser-lobbyOwner");//TODO: get logged user
            var lobby = _lobbyService.Create(gameName, mockedPlayer);
            return RedirectToLobby(lobby.Id, gameName);
        }

        public IActionResult Join(long lobbyId, string gameName)
        {
            var mockedPlayer = new Player("mockedUser-player");
            _lobbyService.Join(lobbyId, gameName, mockedPlayer);
            return RedirectToLobby(lobbyId, gameName);
        }

        private IActionResult RedirectToLobby(long lobbyId, string gameName)
        {
            return RedirectToAction("FindOne", new { gameName = gameName, lobbyId = lobbyId });
        }

        public IActionResult FindOne(long lobbyId, string gameName)
        {
            var lobby = _lobbyService.FindByIdAndGameName(lobbyId, gameName);
            ViewData["loggedUser"] = lobby.ConnectedPlayers.Count == 1 ? "mockedUser-lobbyOwner" : "mockedUser-player"; //TODO: inject logged user
            return View("SingleLobby", lobby);
        }

    }
}
