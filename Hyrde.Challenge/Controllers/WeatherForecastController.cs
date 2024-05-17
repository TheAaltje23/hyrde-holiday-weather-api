using Hyrde.Challenge.Models;
using Microsoft.AspNetCore.Mvc;
using Hyrde.Challenge.Dto;
using Hyrde.Challenge.Services;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Net.Http.Headers;

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
        public async Task<ResponseDto> GetToday(string query)
        {
            _logger.LogInformation($"Received request for weather at {query}");

            if (string.IsNullOrEmpty(query))
            {
                _logger.LogError("Location is required.");
                return new ResponseDto(success: false, data: null, errors: ["Location is required."], validationMessage: "Validation failed.");
            }

            Weather weather = await _service.GetToday(query);

            if (weather == null)
            {
                _logger.LogWarning($"Weather data not found for {query}");
                return new ResponseDto(false, null, ["Location not found."], "Validation failed.");
            }

            var currentWeather = new ReadCurrentDto
            {
                City = weather.City,
                Region = weather.Region,
                Country = weather.Country,
                ConditionText = weather.ConditionText,
                ConditionIcon = weather.ConditionIcon,
                TempCelcius = weather.TempCelcius,
                WindKph = weather.WindKph,
                WindDir = weather.WindDir,
                PrecipitationMm = weather.PrecipitationMm,
                Humidity = weather.Humidity
            };

            _logger.LogInformation($"Weather data retrieved successfully for {query}");
            return new ResponseDto(success: true, data: currentWeather, errors: null, validationMessage: "Weather information retrieved successfully.");
        }

        [HttpGet("GetForecast")]
        public async Task<ResponseDto> GetForecast(string query)
        {
            IEnumerable<Weather> forecast = await _service.GetForecast(query);

            var forecastWeather = forecast.Select(weatherObj => new ReadForecastDto
            {
                ForecastDate = weatherObj.ForecastDate,
                ConditionIcon = weatherObj.ConditionIcon,
                MaxTempCelcius = weatherObj.MaxTempCelcius,
                MinTempCelcius = weatherObj.MinTempCelcius
            });

            _logger.LogInformation($"Weather data retrieved successfully for {query}");
            return new ResponseDto(true, forecastWeather, null, "Weather information retrieved successfully.");
        }
    }
}
