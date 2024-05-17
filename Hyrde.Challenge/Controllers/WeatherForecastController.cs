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
        public async Task<ResponseDto> GetToday(string query)
        {
            _logger.LogInformation("Received request for current weather with query: {Query}", query);

            Weather? weather = await _service.GetToday(query);

            if (weather == null)
            {
                _logger.LogWarning("Current weather data not found or error occurred for query: {Query}", query);
                return new ResponseDto(false, null, ["Location not found or error occurred while retrieving current weather data."], "Validation failed.");
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

            _logger.LogInformation("Current weather data retrieved successfully for query: {Query}", query);
            return new ResponseDto(true, currentWeather, null, "Current weather information retrieved successfully.");
        }

        [HttpGet("GetForecast")]
        public async Task<ResponseDto> GetForecast(string query)
        {
            _logger.LogInformation("Received request for forecast weather with query: {Query}", query);

            IEnumerable<Weather>? forecast = await _service.GetForecast(query);

            if (forecast == null)
            {
                _logger.LogWarning("Forecast weather data not found or error occurred for query: {Query}", query);
                return new ResponseDto(false, null, ["Location not found or error occurred while retrieving forecast weather data."], "Validation failed.");
            }

            var forecastWeather = forecast.Select(weatherObj => new ReadForecastDto
            {
                ForecastDate = weatherObj.ForecastDate,
                ConditionIcon = weatherObj.ConditionIcon,
                MaxTempCelcius = weatherObj.MaxTempCelcius,
                MinTempCelcius = weatherObj.MinTempCelcius
            });

            _logger.LogInformation("Forecast data retrieved successfully for query: {Query}", query);
            return new ResponseDto(true, forecastWeather, null, "Forecast weather information retrieved successfully.");
        }
    }
}