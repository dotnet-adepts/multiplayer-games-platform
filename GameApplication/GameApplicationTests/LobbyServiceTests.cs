using System;
using System.Collections.Generic;
using System.Security.Claims;
using GameApplication.Factories;
using GameApplication.Models.Games;
using GameApplication.Services;
using Moq;
using Xunit;

namespace GameApplicationTests
{
    public class LobbyServiceTests
    {

        private LobbyService _lobbyService;

        private Game _snake;

        private Game _battleships;

        public LobbyServiceTests()
        {
            _lobbyService = new LobbyService(new GameService());
            _snake = new Game(Game.Names.Snake, "Steruj wê¿em, pokonaj innych!", 2, 4, "Zrêcznoœciowe", null);
            _battleships = new Game(Game.Names.Battleship, "Zatop statki przeciwnika zanim on zatopi Twoje!", 2, 2, "Strategiczne", new BatlleshipGameSessionFactory());
        }

        [Fact]
        public void ShouldCreateBattleshipLobby()
        {
            //Arrange
            var mockedClaims = new Mock<ClaimsPrincipal>();
            Player player = new Player(mockedClaims.Object);
            Lobby lobby = new Lobby(_battleships, player);

            //Act
            Lobby receivedLobby = _lobbyService.Create(Game.Names.Battleship, player);

            //Assert
            Assert.NotNull(receivedLobby);
            Assert.Equal(lobby.Game.Category, receivedLobby.Game.Category);
            Assert.Equal(lobby.Game.Name, receivedLobby.Game.Name);
            Assert.Equal(lobby.Id + 1, receivedLobby.Id);

        }

        [Fact]
        public void ShouldCreateSnakeLobby()
        {
            //Arrange
            var mockedClaims = new Mock<ClaimsPrincipal>();
            Player player = new Player(mockedClaims.Object);
            Lobby lobby = new Lobby(_snake, player);

            //Act
            Lobby receivedLobby = _lobbyService.Create(Game.Names.Snake, player);

            //Assert
            Assert.NotNull(receivedLobby);
            Assert.Equal(lobby.Game.Name, receivedLobby.Game.Name);
            Assert.Equal(lobby.Id + 1, receivedLobby.Id);

        }

        [Fact]
        public void ShouldFindEmptyLobbyByGameName()
        {
            //Act
            var lobbies = _lobbyService.FindAllByGameName(Game.Names.Snake);

            //Assert
            Assert.Empty(lobbies);
        }
    }
}
