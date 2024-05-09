using Hyrde.Challenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hyrde.Challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetToday")]
        public Weather GetToday()
        {
            // Replace this implementation with your own.
            return new Weather()
            {
                City = "Maarssen",
                Country = "The Netherlands",
                TempFahrenheit = 55.4f
            };
        }

        [HttpGet("GetForecast")]
        public IEnumerable<Weather> GetForecast()
        {
            // Replace this implementation with your own.
            return new List<Weather>() {
                new Weather() 
                {
                    City = "Maarssen",
                    Country = "The Netherlands",
                    TempFahrenheit = 55.4f
                },
                new Weather() {
                    City = "Maarssen",
                    Country = "The Netherlands",
                    TempFahrenheit = 58.4f
                },
                new Weather() {
                    City = "Maarssen",
                    Country = "The Netherlands",
                    TempFahrenheit = 61.4f
                }
            };
        }
    }
}
