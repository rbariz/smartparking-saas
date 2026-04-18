
namespace SmartParking.Contracts.DriverAuth
{
    public sealed record RequestDriverOtpResponse(
        bool Success,
        string Message,
        int ExpiresInSeconds
    );
}
