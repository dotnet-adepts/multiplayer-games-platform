using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameApplication.Models;

namespace GameApplication.Services
{
    public interface IGameService
    {
        void Save(Game game);

        List<Game> FindAll();

        Game FindById(long Id);
    }
}
