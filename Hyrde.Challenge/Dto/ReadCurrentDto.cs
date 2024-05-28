namespace Hyrde.Challenge.Dto
{
    public class ReadCurrentDto
    {
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }
        public string? ConditionText { get; set; }
        public string? ConditionIcon { get; set; }
        public float Temperature { get; set; }
        public float Wind { get; set; }
        public string? WindDir { get; set; }
        public float Precipitation { get; set; }
        public int Humidity { get; set; }
    }
}