using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMvcAppDemo.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMvcAppDemo.Controllers
{
    public class CommunityCampController : Controller
    {
        private ICommunityCampMemberRepository _repository;
        public CommunityCampController(ICommunityCampMemberRepository repository)
        {
            _repository = repository;
        }
     
        public IActionResult Index()
        {
            var list = _repository.GetAll();

            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CommunityCampJoinMember model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                ModelState.AddModelError("Email", "이메일을 입력하시오.");
            }

            if (ModelState.IsValid)
            {
                ViewBag.Result = $"커뮤니티: {model.CommunityName}, 이름: {model.Name}";

                _repository.AddMember(model);
                TempData["Message"] = "데이터가 저장되었습니다.";

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(CommunityCampJoinMember model)
        {
            if (ModelState.IsValid)
            {
                _repository.DeleteMember(model);
                TempData["Message"] = "데이터가 삭제되었습니다.";

                return RedirectToAction("Index");
            }

            return View();
        }

        //[Authorize]
        //[Authorize("Administrators")]
        public IActionResult ComCampAdmin()
        {
            var list = _repository.GetAll();
            return View(list);
        }
    }
}
