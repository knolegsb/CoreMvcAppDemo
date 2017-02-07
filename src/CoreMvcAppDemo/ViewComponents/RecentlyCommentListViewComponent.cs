using CoreMvcAppDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.ViewComponents
{
    public class RecentlyCommentListViewComponent : ViewComponent
    {
        // 댓글 리파지토리 개체
        private INoteCommentRepository _repository;

        public RecentlyCommentListViewComponent(INoteCommentRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            // 최근 댓글 리스트 전달
            return View(_repository.GetRecentComments());
        }
    }
}
