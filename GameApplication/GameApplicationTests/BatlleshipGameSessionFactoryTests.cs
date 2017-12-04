using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using GameApplication.Factories;
using GameApplication.Models.Games;
using GameApplication.Models.Games.Battleship;
using Moq;
using Xunit;

namespace GameApplicationTests
{
    public class BatlleshipGameSessionFactoryTests
    {
        private BatlleshipGameSessionFactory _factory;

        public BatlleshipGameSessionFactoryTests()
        {
            _factory = new BatlleshipGameSessionFactory();
        }

        [Fact]
        public void ShouldCreateSession()
        {
            //Arrange
            var mockedClaims = new Mock<ClaimsPrincipal>();
            Player player = new Player(mockedClaims.Object);
            Player player2 = new Player(mockedClaims.Object);
            List<Player> players = new List<Player>
            {
                player, player2
            };
            var session = new BattleshipSession(1, player, player2);

            //Act
            var returnedSession = _factory.Create(1, players);

            //Assert
            Assert.Equal(session.getId(), returnedSession.getId());
        }
    }
}
