using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMvcAppDemo.Controllers
{
    public class BoardSummaryModel
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public DateTime PostDate { get; set; }
    }

    public class BoardSummaryRepository
    {
        public List<BoardSummaryModel> GetAll()
        {
            var lists = new List<BoardSummaryModel>()
            {
                new Controllers.BoardSummaryModel
                { Id = 1, Alias = "Notice", Name = "홍길동", Title = "공지사항입니다.", PostDate = DateTime.Now },
                new Controllers.BoardSummaryModel
                { Id = 1, Alias = "Free", Name = "백두산", Title = "자유게시판입니다.", PostDate = DateTime.Now },
                new Controllers.BoardSummaryModel
                { Id = 1, Alias = "Photo", Name = "임꺽정", Title = "사진게시판입니다.", PostDate = DateTime.Now },
                new Controllers.BoardSummaryModel
                { Id = 1, Alias = "Notice", Name = "홍길동", Title = "공지사항입니다.", PostDate = DateTime.Now }
            };
            return lists;
        }

        public List<BoardSummaryModel> GetByAlias(string alias)
        {
            return GetAll().Where(b => b.Alias == alias).ToList();
        }
    }

    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ApiBoardSummaryController : Controller
    {
        private BoardSummaryRepository _repository;

        public ApiBoardSummaryController()
        {
            _repository = new BoardSummaryRepository();
        }
        
        // GET: api/values
        [HttpGet]
        public IEnumerable<BoardSummaryModel> Get()
        {
            return _repository.GetAll();
        }

        [HttpPost("{alias}", Name = "Get")]
        public IEnumerable<BoardSummaryModel> Get(string alias)
        {
            return _repository.GetByAlias(alias);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class BoardSummaryDemoController : Controller
    {
        public IActionResult Index()
        {
            string html = @"
                            <div id='print'></div>
                            <script src='https://code.jquery.com/jquery-2.2.4.min.js'></script>
                            <script>
                            $(function() {
                                $.getJSON('/api/BoardSummaryApi', function(data){
                                    var str = '<dl>';
                                    $.each(data, function (index, entry){
                                        str += '<dt>' + entry.title + '</dt><dd>' + entry.name + '</dd>';
                                    });

                                    str += '</dl>';
                                    $('#print').html(str);
                                });
                            });    
                            </script>
                           ";

            ContentResult cr = new ContentResult()
            {
                ContentType = "text/html",
                Content = html
            };
            return cr;
        }
    }
}
