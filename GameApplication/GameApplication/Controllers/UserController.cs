using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameApplication.Models;
using GameApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameApplication.Controllers
{
    [Route("/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public List<User> FindAll()
        {
            return _userService.FindAll();
        }

        [HttpGet("{id}", Name ="GetUser")]
        public IActionResult FindById(long id)
        {
            var user = _userService.FindById(id);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            _userService.Save(user);
            return CreatedAtRoute("GetUser", new { id = user.UserId }, user);
        }
    }
}