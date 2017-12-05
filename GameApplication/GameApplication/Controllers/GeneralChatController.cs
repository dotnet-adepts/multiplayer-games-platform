using System;
using System.Diagnostics;
using GameApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameApplication.Controllers
{
    public class GeneralChatController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
