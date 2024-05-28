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
        public async Task<ResponseDto> GetToday(string query, string unit)
        {
            _logger.LogInformation("Received request for current weather with query: {Query}", query);

            Weather? weather = await _service.GetToday(query, unit);

            if (weather == null)
            {
                _logger.LogWarning("Current weather data not found or error occurred for query: {Query}", query);
                return new ResponseDto(false, null, ["Location not found or error occurred while retrieving current weather data"], "Validation failed");
            }

            var currentWeather = new ReadCurrentDto
            {
                City = weather.City,
                Region = weather.Region,
                Country = weather.Country,
                ConditionText = weather.ConditionText,
                ConditionIcon = weather.ConditionIcon,
                Temperature = unit.Equals("c", StringComparison.OrdinalIgnoreCase) ? weather.TempCelcius : weather.TempFahrenheit,
                Wind = unit.Equals("c", StringComparison.OrdinalIgnoreCase) ? weather.WindKph : weather.WindMph,
                WindDir = weather.WindDir,
                Precipitation = unit.Equals("c", StringComparison.OrdinalIgnoreCase) ? weather.PrecipitationMm : weather.PrecipitationIn,
                Humidity = weather.Humidity
            };

            _logger.LogInformation("Current weather data retrieved successfully for query: {Query}", query);
            return new ResponseDto(true, currentWeather, null, "Current weather information retrieved successfully");
        }

        [HttpGet("GetForecast")]
        public async Task<ResponseDto> GetForecast(string query, string unit)
        {
            _logger.LogInformation("Received request for forecast weather with query: {Query}", query);

            IEnumerable<Weather>? forecast = await _service.GetForecast(query, unit);

            if (forecast == null)
            {
                _logger.LogWarning("Forecast weather data not found or error occurred for query: {Query}", query);
                return new ResponseDto(false, null, ["Location not found or error occurred while retrieving forecast weather data"], "Validation failed");
            }

            var forecastWeather = forecast.Select(weatherObj => new ReadForecastDto
            {
                ForecastDate = weatherObj.ForecastDate,
                ConditionIcon = weatherObj.ConditionIcon,
                MaxTemperature = unit.Equals("c", StringComparison.OrdinalIgnoreCase) ? weatherObj.MaxTempCelcius : weatherObj.MaxTempFahrenheit,
                MinTemperature = unit.Equals("c", StringComparison.OrdinalIgnoreCase) ? weatherObj.MinTempCelcius : weatherObj.MinTempFahrenheit,
                ChanceOfRain = weatherObj.ChanceOfRain
            });

            _logger.LogInformation("Forecast data retrieved successfully for query: {Query}", query);
            return new ResponseDto(true, forecastWeather, null, "Forecast weather information retrieved successfully");
        }

        [HttpGet("GetHourly")]
        public async Task<ResponseDto> GetHourly(string query, string unit)
        {
            _logger.LogInformation("Received request for hourly weather with query: {Query}", query);

            IEnumerable<Weather>? hourly = await _service.GetHourly(query, unit);

            if (hourly == null)
            {
                _logger.LogWarning("Hourly weather data not found or error occurred for query: {Query}", query);
                return new ResponseDto(false, null, ["Location not found or error occurred while retrieving hourly weather data"], "Validation failed");
            }

            var hourlyWeather = hourly.Select(weatherObj => new ReadHourlyDto
            {
                ForecastHour = weatherObj.ForecastHour,
                ConditionIcon = weatherObj.ConditionIcon,
                Temperature = unit.Equals("c", StringComparison.OrdinalIgnoreCase) ? weatherObj.TempCelcius : weatherObj.TempFahrenheit,
                ChanceOfRain = weatherObj.ChanceOfRain,
                Precipitation = unit.Equals("c", StringComparison.OrdinalIgnoreCase) ? weatherObj.PrecipitationMm : weatherObj.PrecipitationIn,
                Wind = unit.Equals("c", StringComparison.OrdinalIgnoreCase) ? weatherObj.WindKph : weatherObj.WindMph,
                WindDir = weatherObj.WindDir
            });

            _logger.LogInformation("Hourly data retrieved successfully for query: {Query}", query);
            return new ResponseDto(true, hourlyWeather, null, "Hourly weather information retrieved successfully");
        }
    }
}