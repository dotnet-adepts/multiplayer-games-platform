using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameApplication.Data;
using GameApplication.Models;
using GameApplication.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace GameApplication.Services
{
    public class GameService : IGameService
    {

        private readonly IGameRepository _gameRepository;
        private readonly ILogger _logger;

        public GameService(IGameRepository gameRepository, ILogger<GameService> logger)
        {
            _gameRepository = gameRepository;
            _logger = logger;
        }

        public List<Game> FindAll()
        {
            return _gameRepository.FindAll();
        }

        public Game FindById(long Id)
        {
            return _gameRepository.FindById(Id);
        }

        public void Save(Game game)
        {
            _gameRepository.Save(game);
        }
    }
}

