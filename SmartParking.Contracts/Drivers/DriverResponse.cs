namespace SmartParking.Contracts.Drivers
{
    public sealed record DriverResponse(
    Guid Id,
    string FullName,
    string Phone,
    string? Email,
    DateTime CreatedAtUtc
);
}
