﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GameApplication.Controllers
{
    public class TemplateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CircleSection()
        {
            return View();
        }

        public IActionResult DiagramSection()
        {
            return View();
        }

        public IActionResult FormSection()
        {
            return View();
        }

        public IActionResult UpdatesSection()
        {
            return View();
        }
    }
}