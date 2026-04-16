namespace SmartParking.Contracts.Drivers
{
    public sealed record DriverCreateRequest(
    string FullName,
    string Phone,
    string? Email
);
}
