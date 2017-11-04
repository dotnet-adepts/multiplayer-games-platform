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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public List<User> FindAll()
        {
            _logger.LogTrace("Finding all users");
            return _userRepository.FindAll();
        }

        public User FindById(long ID)
        {
            _logger.LogTrace("Finding user by ID: ", ID);
            return _userRepository.FindById(ID);
        }

        public void Save(User user)
        {
            _logger.LogTrace("Saving new user");
            _userRepository.Save(user);
        }
    }
}
