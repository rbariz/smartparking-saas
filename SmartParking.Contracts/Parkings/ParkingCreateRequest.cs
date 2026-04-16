namespace SmartParking.Contracts.Parkings
{
    public sealed record ParkingCreateRequest(
    string Name,
    string Address,
    decimal Latitude,
    decimal Longitude,
    int TotalCapacity,
    string Currency,
    decimal HourlyRate
);
}
