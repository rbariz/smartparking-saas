namespace SmartParking.Contracts.Bookings
{
    public sealed record BookingCreateRequest(
    Guid DriverId,
    Guid ParkingId,
    DateTime StartTime,
    DateTime EndTime
);



}
