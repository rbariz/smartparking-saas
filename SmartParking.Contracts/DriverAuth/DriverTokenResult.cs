
namespace SmartParking.Contracts.DriverAuth
{
    public sealed record DriverTokenResult(
    string AccessToken,
    DateTime ExpiresAtUtc
);
}
