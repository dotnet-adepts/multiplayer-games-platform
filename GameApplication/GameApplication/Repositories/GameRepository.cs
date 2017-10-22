using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameApplication.Data;
using GameApplication.Models;
using GameApplication.Repositories.Interfaces;

namespace GameApplication.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GameContext _context;

        public GameRepository(GameContext context)
        {
            _context = context;
        }

        public List<Game> FindAll()
        {
            return _context.Games.ToList<Game>();
        }

        public Game FindById(long Id)
        {
            return _context.Games.FirstOrDefault(game => game.GameId == Id);
        }

        public void Save(Game game)
        {
            _context.Add(game);
            _context.SaveChanges();
        }
    }
}
