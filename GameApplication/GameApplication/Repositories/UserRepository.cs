using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameApplication.Data;
using GameApplication.Models;
using GameApplication.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GameContext _context;

        public UserRepository(GameContext context)
        {
            _context = context;
        }

        public List<User> FindAll()
        {
            return _context.Users.ToList<User>();
        }

        public User FindById(long Id)
        {
            return _context.Users.FirstOrDefault(user => user.UserId == Id);
        }

        public void Save(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }
    }
}
