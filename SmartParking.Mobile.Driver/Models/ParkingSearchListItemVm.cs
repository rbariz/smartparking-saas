namespace SmartParking.Mobile.Driver.Models
{
    public sealed class ParkingSearchListItemVm
    {
        public Guid ParkingId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int AvailableCount { get; set; }
        public int TotalCapacity { get; set; }
        public decimal HourlyRate { get; set; }
        public string Currency { get; set; } = string.Empty;
        public decimal EstimatedPrice { get; set; }
        public double DistanceKm { get; set; }
        public string DistanceLabel { get; set; } = string.Empty;

        public bool IsBestOption { get; set; }
        public bool IsCheapest { get; set; }
    }
}
