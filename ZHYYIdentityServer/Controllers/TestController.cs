using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZHYYDBSqlServerHIS;
using ZHYYDBSqlServerHIS.Interfaces;
using ZHYYDBSqlServerHIS.SqlSplit;

namespace ZHYYIdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public OrclHelper helper = new OrclHelper();
        public IUser user { get; }

        public TestController(IUser user)
        {
            this.user = user;
        }
        // GET: api/Test
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(user.GetUsers());
        }

        // GET: api/Test/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Test
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
       
    }
}
