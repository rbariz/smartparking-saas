namespace SmartParking.Mobile.Driver.Services
{
    public sealed class SearchState
    {
        public decimal Latitude { get; set; } = 33.589886m;
        public decimal Longitude { get; set; } = -7.603869m;
        public int RadiusMeters { get; set; } = 1000;
        public DateTime StartTime { get; set; } = DateTime.UtcNow.AddMinutes(10);
        public DateTime EndTime { get; set; } = DateTime.UtcNow.AddHours(2);
    }


}
