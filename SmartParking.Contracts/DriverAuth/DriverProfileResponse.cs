
namespace SmartParking.Contracts.DriverAuth
{
    public sealed record DriverProfileResponse(
       Guid Id,
    string FullName,
    string Phone,
    string? Email
    );
}
