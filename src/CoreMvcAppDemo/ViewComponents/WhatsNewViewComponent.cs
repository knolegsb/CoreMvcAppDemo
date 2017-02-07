using CoreMvcAppDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.ViewComponents
{
    public class WhatsNewViewComponent : ViewComponent
    {
        // 게시판 리파지터리 개체
        private INoteRepository _repository;

        public WhatsNewViewComponent(INoteRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            // return View("Details", _repository.GetRecentPosts()); // 이름 지정
            return View(_repository.GetRecentPosts()); // 기본값: Default
        }
    }
}
