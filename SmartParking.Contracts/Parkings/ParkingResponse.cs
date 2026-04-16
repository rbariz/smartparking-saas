namespace SmartParking.Contracts.Parkings
{
    public sealed record ParkingResponse(
    Guid Id,
    Guid OperatorId,
    string Name,
    string Address,
    decimal Latitude,
    decimal Longitude,
    int TotalCapacity,
    int AvailableCount,
    string Status,
    string Currency,
    decimal HourlyRate,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc
);
}
