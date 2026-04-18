
namespace SmartParking.Contracts.DriverAuth
{
    public sealed record DriverAuthResponse(
        string AccessToken,
        DateTime ExpiresAtUtc,
        DriverProfileResponse Driver
    );
}
