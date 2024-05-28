using Hyrde.Challenge.Models;

namespace Hyrde.Challenge.Services
{
    public interface IWeatherService
    {
        Task<Weather?> GetToday(string query, string unit);
        Task<IEnumerable<Weather>?> GetForecast(string query, string unit);
        Task<IEnumerable<Weather>?> GetHourly(string query, string unit);
    }
}