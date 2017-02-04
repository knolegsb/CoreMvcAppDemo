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
    public class ApiHelloValueController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<Value> Get()
        {
            return new Value[] {
                new Value { Id = 1, Text = "안녕하세요" },
                new Value { Id = 2, Text = "반갑습니다" },
                new Value { Id = 3, Text = "또 만나요" }
            };
        }

        // GET api/values/5
        [HttpGet("{id:int}")]
        public Value Get(int id)
        {
            return new Value { Id = id, Text = $"넘어온 값: {id}" };
        }

        // POST api/values
        [HttpPost]
        [Produces("application/json", Type=typeof(Value))]
        [Consumes("appliaction/json")]
        public IActionResult Post([FromBody]Value value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return CreatedAtAction("Get", new { id = value.Id }, value);
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
