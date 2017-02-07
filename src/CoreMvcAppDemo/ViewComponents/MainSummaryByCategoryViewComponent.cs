using CoreMvcAppDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.ViewComponents
{
    public class MainSummaryByCategoryViewComponent : ViewComponent
    {
        private INoteRepository _repository;

        public MainSummaryByCategoryViewComponent(INoteRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke(string category)
        {
            // 이름 지정
            // return View("Details", _repository.GetRecentPosts());
            // 기본값: Default
            return View(_repository.GetNoteSummaryByCategory(category));
        }
    }
}
