using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMvcAppDemo.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMvcAppDemo.Controllers
{
    [Route("api/[controller]")]
    public class WebCampServiceController : Controller
    {
        [HttpGet]
        public IEnumerable<WebCampSpeaker> Get()
        {
            var lists = new List<WebCampSpeaker>();

            lists.Add(new WebCampSpeaker
            {
                Id = 1,
                Name = "박용준 MVP",
                Photo = "박용준 MVP",
                Title = "MVP",
                Description = "데브렉 전임강사"
            });
            lists.Add(new WebCampSpeaker
            {
                Id = 2,
                Name = "김태영 부장",
                Photo = "김태영 부장",
                Title = "Microsoft",
                Description = "한국 마이크로소프트 DX, Technical Evangelist"
            });
            lists.Add(new WebCampSpeaker
            {
                Id = 3,
                Name = "김명신 부장",
                Photo = "김명신 부장",
                Title = "Microsoft",
                Description = "한국 마이크로소프트 DX, Technical Evangelist"
            });
            lists.Add(new WebCampSpeaker
            {
                Id = 4,
                Name = "한상훈 MVP",
                Photo = "한상훈 MVP",
                Title = "MVP",
                Description = "넥슨 개발자"
            });
            lists.Add(new WebCampSpeaker
            {
                Id = 1,
                Name = "성지용 부장",
                Photo = "성지용 부장",
                Title = "Microsoft",
                Description = "한국 마이크로소프트 DX, Technical Evangelis"
            });
            return lists;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
}
