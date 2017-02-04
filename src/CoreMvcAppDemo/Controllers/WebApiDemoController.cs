using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMvcAppDemo.Controllers
{
    [Route("api/[controller]")]
    public class WebApiDemoController : Controller
    {
        // GET: api/values
        [HttpGet]
        public JsonResult Get()
        {
            return Json(new { Name = "귀염둥이" });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody]WebApiDemoClass name)
        {
            if (ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.Created;
                return Json(true);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("실패");
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

    public class WebApiDemoClass
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "3자 이상")]
        public string Name { get; set; }
    }
}
