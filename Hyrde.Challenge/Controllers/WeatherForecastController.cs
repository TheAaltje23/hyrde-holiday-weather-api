using Hyrde.Challenge.Models;
using Microsoft.AspNetCore.Mvc;
using Hyrde.Challenge.Dto;
using Microsoft.Extensions.Primitives;
using Hyrde.Challenge.Services;

namespace Hyrde.Challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IWeatherService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("GetToday")]
        public async Task<ResponseDto> GetToday(string cityName)
        {
            if (string.IsNullOrEmpty(cityName))
            {
                return new ResponseDto(success: false, data: null, errors: ["City name is required."], validationMessage: "City name is required.");
            }

            Weather weather = await _service.GetToday(cityName);
            if (weather == null)
            {
                return new ResponseDto(success: false, data: null, errors: new List<string> { "City not found." }, validationMessage: "City not found.");
            }

            return new ResponseDto(success: true, data: weather, errors: null, validationMessage: "Weather information retrieved successfully.");
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
                },
                new Weather() {
                    City = "Maarssen",
                    Country = "The Netherlands",
                },
                new Weather() {
                    City = "Maarssen",
                    Country = "The Netherlands",
                }
            };
        }
    }
}
