using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameApplication.Models
{
    public class Game
    {
        public long GameId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public GameType Type { get; set; }

        public List<UserGame> Players { get; set; }
    }
}
