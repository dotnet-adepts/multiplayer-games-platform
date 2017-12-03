using System;
using System.Collections.Generic;
using System.Text;
using GameApplication.Factories;
using GameApplication.Models.Games;
using GameApplication.Services;
using Xunit;

namespace GameApplicationTests
{

    public class GameServiceTests
    {
        private GameService _gameService;

        public GameServiceTests()
        {
            _gameService = new GameService();
        }

        [Fact]
        public void ShouldFindAllGamesNames()
        {
            //Arrange
            List<string> games = new List<string>
            {
                Game.Names.Snake,
                Game.Names.Battleship
            };

            //Act
            var gamesNames = _gameService.FindAllGamesNames();

            //Assert
            Assert.Equal(gamesNames, games);
        }

        [Fact]
        public void ShouldFindByGameName()
        {
            //Arrange
            var game = new Game(Game.Names.Battleship, "dluga nazwa", 2, 2, "Strategiczne", new BatlleshipGameSessionFactory());

            //Act
            var battleshipGame = _gameService.FindByGameName(Game.Names.Battleship);

            //Assert
            Assert.NotNull(battleshipGame);
            Assert.Equal(battleshipGame.Category, game.Category);
            Assert.Equal(battleshipGame.MaxNumberOfPlayers, game.MaxNumberOfPlayers);
            Assert.Equal(battleshipGame.Name, game.Name);
        }
    }
}
