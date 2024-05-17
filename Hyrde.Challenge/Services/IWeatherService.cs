using Hyrde.Challenge.Models;

namespace Hyrde.Challenge.Services
{
    public interface IWeatherService
    {
        Task<Weather?> GetToday(string query);
        Task<IEnumerable<Weather>?> GetForecast(string query);
    }
}