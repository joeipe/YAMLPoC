using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YAMLPoC.API.KeyVault;

namespace YAMLPoC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _config;
        private readonly IKeyVaultManager _secretManager;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IConfiguration config,
            IKeyVaultManager secretManager)
        {
            _logger = logger;
            _config = config;
            _secretManager = secretManager;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> GetWeatherForecast()
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

        [HttpGet]
        public ActionResult GetConfig()
        {
            using (_logger.BeginScope("GetConfig"))
            _logger.LogInformation("This is a log message. This is an object: {User}", new { name = "Joe Ipe" });
            var list = new List<string>
            {
                _config["ApiSettings:FromConfig"],
                _config["ApiSettings:FromLibrary"],
                _config["ApiSettings:FromVault"],
            };

            return Ok(list);
        }

        [HttpGet]
        public async Task<ActionResult> GetVault()
        {
            using (_logger.BeginScope("GetVault"))
            _logger.LogInformation("This is a log message. This is an object: {User}", new { name = "Joe Ipe" });

            string secretValue = await _secretManager.GetSecret("SuperSecret");
            var list = new List<string>
            {
                secretValue
            };

            return Ok(list);
        }
    }
}
