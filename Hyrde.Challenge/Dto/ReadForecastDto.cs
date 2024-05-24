namespace Hyrde.Challenge.Dto
{
    public class ReadForecastDto
    {
        public string? ForecastDate { get; set; }
        public string? ConditionIcon { get; set; }
        public float MaxTemperature { get; set; }
        public float MinTemperature { get; set; }
        public int ChanceOfRain { get; set; }
    }
}