namespace SmartParking.Contracts.Bookings
{
    public sealed record BookingResponse(
    Guid Id,
    Guid DriverId,
    Guid ParkingId,
    DateTime StartTime,
    DateTime EndTime,
    string Status,
    decimal ReservedPrice,
    string PriceCurrency,
    DateTime ExpiresAtUtc,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc
);



}
