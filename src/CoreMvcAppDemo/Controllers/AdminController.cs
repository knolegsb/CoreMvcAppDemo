using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMvcAppDemo.Controllers
{
    public class AdminController : Controller
    {
        //[Authorize(Roles = "Users")]
        //[Authorize("Users")] // Startup.cs AddPolicy() 설정 필요
        //[Authorize("Administrators")]
        //public string Index()
        //{
        //    return "Admin 사용자만 볼 수 있음";
        //}
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 게시판 관리자 페이지
        /// </summary>
        /// <returns></returns>
        public IActionResult NoteManager()
        {
            return View();
        }

        /// <summary>
        /// 사용자 관리자 페이지
        /// </summary>
        /// <returns></returns>
        public IActionResult UserManager()
        {
            return View();
        }
    }
}
