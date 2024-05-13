using Hyrde.Challenge.Models;
using Microsoft.AspNetCore.Mvc;
using Hyrde.Challenge.Dto;
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
            _logger.LogInformation($"Received request for weather in {cityName}");

            if (string.IsNullOrEmpty(cityName))
            {
                _logger.LogError("City name is required.");
                return new ResponseDto(success: false, data: null, errors: ["City name is required."], validationMessage: "Validation failed.");
            }

            Weather weather = await _service.GetToday(cityName);

            if (weather == null)
            {
                _logger.LogWarning($"Weather data not found for {cityName}");
                return new ResponseDto(success: false, data: null, errors: new List<string> { "City not found." }, validationMessage: "Validation failed.");
            }

            _logger.LogInformation($"Weather data retrieved successfully for {cityName}");
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
