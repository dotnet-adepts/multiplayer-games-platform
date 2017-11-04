using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameApplication.Models;
using GameApplication.Data;
using GameApplication.Services;

namespace GameApplication.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUserService _userService;
        private readonly IGameService _gameService;

        public HomeController(IUserService userService, IGameService gameService)
        {
            _userService = userService;
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
