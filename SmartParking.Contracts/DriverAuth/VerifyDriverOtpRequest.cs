
namespace SmartParking.Contracts.DriverAuth
{
    public sealed record VerifyDriverOtpRequest(
        string Contact,
        string Code,
        string? FullName = null
    );
}
