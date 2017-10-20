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

            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
                {
                    new User{Username="user1",Password="pass1"},
                    new User{Username="user2",Password="pass2"},
                    new User{Username="user3",Password="pass3",},
                    new User{Username="maciej",Password="rutkowski"}
                };

            foreach (User user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
