using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameApplication.Controllers;
using GameApplication.Controllers.GamesSession;
using GameApplication.Factories;
using GameApplication.Models.Games;
using GameApplication.Services.GamesSessions;

namespace GameApplication.Services
{
    public class GameService
    {
        private readonly List<Game> _games;

        public GameService()
        {
            var snake = new Game(Game.Names.Snake , "Steruj wężem, pokonaj innych!", 2, 4, "Zręcznościowe", null); //TODO : implement or delete
            var battleship = new Game(Game.Names.Battleship, "Zatop statki przeciwnika zanim on zatopi Twoje!", 2, 2, "Strategiczne", new BatlleshipGameSessionFactory());
            this._games = new List<Game> { snake, battleship };
        }

        public List<Game> FindAll()
        {
            return _games;
        }

        public List<string> FindAllGamesNames()
        {
           return (from game in _games select game.Name).ToList();
        }

        public Game FindByGameName(string gameName)
        {
            return _games.Find(game => game.Name.Equals(gameName));
        }
    }
}
