using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameApplication.Models;

namespace GameApplication.Services
{
    public interface IUserService
    {
        void Save(User user);

        List<User> FindAll();

        User FindById(long ID);
    }
}
