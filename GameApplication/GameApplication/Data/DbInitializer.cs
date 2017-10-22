using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameApplication.Models;

namespace GameApplication.Data
{
    public static class DbInitializer
    {
        public static void Initialize(GameContext context)
        {
            context.Database.EnsureCreated();

            User user = new User { Username = "user1", Password = "pass1" };
            Game game = new Game { StartDate = DateTime.Now, FinishDate = DateTime.Now.AddDays(2), Type = GameType.SNAKE };
            user.Games = new List<UserGame>
            {
                new UserGame { User = user, Game = game }
            };
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
