namespace SmartParking.Contracts.Bookings
{
    public sealed record DriverBookingListItemResponse(
    Guid Id,
    Guid ParkingId,
    string ParkingName,
    DateTime StartTime,
    DateTime EndTime,
    string Status,
    decimal ReservedPrice,
    string PriceCurrency,
    Guid? PaymentId,
    string? PaymentStatus
);



}
