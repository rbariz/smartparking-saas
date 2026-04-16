namespace SmartParking.Contracts.Parkings
{
    public sealed record ParkingAvailabilityUpdateRequest(
    bool IsOpen,
    int AvailableCount
);
}
