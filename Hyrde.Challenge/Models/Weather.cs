namespace Hyrde.Challenge.Models
{
    public class Weather
    {
        public float TempCelcius { get; set; }
        public float TempFahrenheit { get; set; }
        public string? ConditionText { get; set; }
        public string? ConditionIcon { get; set; }
        public float WindKph { get; set; }
        public float PrecipitationMm { get; set; }
        public int Humidity { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? WindDir { get; set; }
        public string? ForecastDate { get; set; }
        public float MaxTempCelcius { get; set; }
        public float MinTempCelcius { get; set; }
        public float MaxTempFahrenheit { get; set; }
        public float MinTempFahrenheit { get; set; }
        public int ChanceOfRain { get; set; }

    }
}