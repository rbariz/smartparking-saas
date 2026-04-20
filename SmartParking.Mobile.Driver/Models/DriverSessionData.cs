namespace SmartParking.Mobile.Driver.Models
{
    public sealed record DriverSessionData(
        Guid DriverId,
        string? DriverName,
        string? Phone,
        string? Email,
        string? AccessToken,
        DateTime? ExpiresAtUtc
    );

}
