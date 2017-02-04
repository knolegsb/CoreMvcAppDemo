using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CoreMvcAppDemo.Models;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMvcAppDemo.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }
     
        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_repository.GetUserByUserId(model.UserId).UserId != null)
                {
                    ModelState.AddModelError("", "이미 가입된 사용자입니다.");
                    return View(model);
                }
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "잘못된 가입 시도!!!");
                return View(model);
            }
            else
            {
                _repository.AddUser(model.UserId, model.Password);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                if (_repository.IsCorrectUser(model.UserId, model.Password))
                {
                    var claims = new List<Claim>()
                    {
                        new Claim("UserId", model.UserId),
                        new Claim(ClaimTypes.Role, "Users")
                    };

                    var ci = new ClaimsIdentity(claims, model.Password);

                    await HttpContext.Authentication.SignInAsync("Cookies", new ClaimsPrincipal(ci));

                    return LocalRedirect("/User/Index");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            return Redirect("/User/Index");
        }

        [Authorize]
        public IActionResult UserInfo()
        {
            return View();
        }

        public IActionResult Greetings()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return new ChallengeResult();
            }

            return View();
        }

        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
