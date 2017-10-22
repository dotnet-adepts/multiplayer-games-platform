using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameApplication.Models
{
    public class UserGame
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public long GameId { get; set; }
        public Game Game { get; set; }
    }
}
