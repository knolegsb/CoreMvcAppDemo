﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMvcAppDemo.Controllers
{
    public class ViewComponentDemoController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CopyrightDemo()
        {
            return View();
        }

        public IActionResult TechListDemo()
        {
            return View();
        }

        public IActionResult SiteListDemo()
        {
            return View();
        }
    }
}
