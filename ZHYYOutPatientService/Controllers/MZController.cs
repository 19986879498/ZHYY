using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZHYYDBSqlServerHIS.Interfaces;

namespace ZHYYOutPatientService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MZController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<MZController> _logger;

        public IUser register { get; }

        public MZController(ILogger<MZController> logger,  IUser iregister)
        {
            _logger = logger;
            this.register = iregister;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet,Route("HIS")]
        public IActionResult HIS()
        {
            return new ObjectResult(register.GetUsers());
        }
    }
}
