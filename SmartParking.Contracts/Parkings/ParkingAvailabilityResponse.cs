namespace SmartParking.Contracts.Parkings
{
    public sealed record ParkingAvailabilityResponse(
    Guid ParkingId,
    string Status,
    int TotalCapacity,
    int AvailableCount,
    DateTime UpdatedAtUtc
);
}
