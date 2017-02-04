using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMvcAppDemo.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMvcAppDemo.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ApiTechesController : Controller
    {
        private ITechRepository _repo;

        public ApiTechesController(ITechRepository repo)
        {
            _repo = repo;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Tech> GetTech()
        {
            return _repo.GetTeches();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public Tech PostTech([FromBody]Tech tech)
        {
            _repo.AddTech(tech);
            return tech;
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
