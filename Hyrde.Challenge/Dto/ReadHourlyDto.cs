namespace Hyrde.Challenge.Dto
{
    public class ReadHourlyDto
    {
        public string? ForecastHour { get; set; }
        public string? ConditionIcon { get; set; }
        public float Temperature { get; set; }
        public int ChanceOfRain { get; set; }
        public float PrecipitationMm { get; set; }
        public float Wind { get; set; }
        public string? WindDir { get; set; }
    }
}