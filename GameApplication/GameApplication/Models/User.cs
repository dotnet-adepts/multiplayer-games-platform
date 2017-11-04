using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameApplication.Models
{
    public class User
    {
        public long UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public List<UserGame> Games { get; set; }
    }
}
