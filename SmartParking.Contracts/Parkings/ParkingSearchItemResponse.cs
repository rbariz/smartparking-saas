namespace SmartParking.Contracts.Parkings
{
    public sealed record ParkingSearchItemResponse(
    Guid ParkingId,
    string Name,
    string Address,
    decimal Latitude,
    decimal Longitude,
    int DistanceMeters,
    string Status,
    int AvailableCount,
    int TotalCapacity,
    decimal HourlyRate,
    string Currency,
    decimal EstimatedPrice
);
}
