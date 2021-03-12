using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ZHYYDBSqlServerHIS;
using ZHYYDBSqlServerHIS.Interfaces;

namespace ZHYYRegisterService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUser register;

        public IConfiguration config { get; }
        public DB db { get; }

        public RegisterController( IUser register,IConfiguration configuration,DB dB)
        {
            this.register = register;
            this.config = configuration;
            this.db = dB;
        }
        // GET: api/Register
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(this.register.GetUsers());
        }

        // GET: api/Register/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Register
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Register/5
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
