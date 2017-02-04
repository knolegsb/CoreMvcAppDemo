using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMvcAppDemo.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMvcAppDemo.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Index()
        {
            DataService demoService = new DataService();
            var data = demoService.GetAll();
            return View(data);
        }

        public IActionResult ContentResultDemo()
        {
            return Content("ContentResult Return Value");
        }
    }
}
