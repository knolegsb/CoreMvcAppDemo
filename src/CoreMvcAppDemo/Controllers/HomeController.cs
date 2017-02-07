using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMvcAppDemo.Settings;
using CoreMvcAppDemo.Models;
using Microsoft.Extensions.Options;

namespace CoreMvcAppDemo.Controllers
{
    public class HomeController : Controller
    {
        // 강력한 형식의 클래스의 인스턴스 생성
        private CoreMvcAppDemoSettings _dnnSettings;
        private INoteRepository _repository; // 게시판
        private INoteCommentRepository _commentRepo; // 댓글

        public HomeController(IOptions<CoreMvcAppDemoSettings> options, INoteRepository repository, INoteCommentRepository commentRepo)
        {
            _dnnSettings = options.Value; // value 속성으로 인스턴스화된 개체 반환
            _repository = repository;
            _commentRepo = commentRepo;
        }

        public IActionResult Index()
        {
            // ViewData[] 또는 ViewBag. 개제로 뷰 페이지로 값 전송
            ViewBag.SiteName = _dnnSettings.SiteName;
            ViewBag.SiteUrl = _dnnSettings.SiteUrl;

            ViewData["Photos"] = _repository.GetNewPhotos();
            ViewData["Notice"] = _repository.GetNoteSummaryByCategory("Notice"); // 공지사항
            ViewData["Free"] = _repository.GetNoteSummaryByCategory("Free"); // 자유게시판
            ViewData["Data"] = _repository.GetNoteSummaryByCategory("Data"); // 자료실
            ViewData["QnA"] = _repository.GetNoteSummaryByCategory("QnA"); // Q & A
            ViewData["RecentPost"] = _repository.GetRecentPosts(); // 최근 글 리스트
            ViewData["RecentComment"] = _commentRepo.GetRecentComments(); // 최근 댓글 리스트

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }
    }
}
