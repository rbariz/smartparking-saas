namespace SmartParking.Contracts.Parkings
{
    public sealed record ParkingQuoteResponse(
    Guid ParkingId,
    DateTime StartTime,
    DateTime EndTime,
    int BillableHours,
    decimal HourlyRate,
    decimal EstimatedPrice,
    string Currency
);
}
