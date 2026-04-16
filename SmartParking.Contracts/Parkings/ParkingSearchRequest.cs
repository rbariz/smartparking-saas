namespace SmartParking.Contracts.Parkings
{
    public sealed record ParkingSearchRequest(
    decimal Latitude,
    decimal Longitude,
    int RadiusMeters,
    DateTime? StartTime,
    DateTime? EndTime,
    bool OpenOnly,
    string? SortBy
);
}
