using CoreMvcAppDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.ViewComponents
{
    public class SiteListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var siteLists = new List<Site>()
            {
                new Site { Id = 1, Title = "길벗출판사", Url = "http://www.gilbut.co.kr", Description = "ASP.NET Core 1.0 서적 출판" },
                new Site { Id = 2, Title = "데브렉", Url = "http://www.devlec.com", Description = "DotNetNote 사이트 제작 관련 동영상 강의 제공" },
                new Site { Id = 3, Title = "Taeyo.NET", Url = "http://www.taeyo.net", Description = "ASP.NET Core 1.0 강좌 제공" },
                new Site { Id = 4, Title = "ASP.NET Korea User Group", Url = "http://www.facebook.com/groups/aspxkorea/", Description = "ASP.NET 한국 사용자 그룹" },
                new Site { Id = 5, Title = "닷넷코리아", Url = "http://www.dotnetkorea.com", Description = "박용준 MVP 개인 홈페이지" },
                new Site { Id = 6, Title = "비주얼아카데미", Url = "http://www.youtube.com/user/visualacademy", Description = "박용준 MVP 개인 유투브 채널" },
                new Site { Id = 7, Title = "ASP.NET", Url = "http://www.asp.net", Description = "ASP.NET 공식 사이트" },
            };
            return View(siteLists);
        }
    }
}
