using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.ViewComponents
{
    public class CopyrightViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            string viewName = "Default";
            if (DateTime.Now.Second % 2 == 0)
            {
                viewName = "Details";
            }
            return View(viewName);
        }
    }
}
