namespace SmartParking.Mobile.Driver.Services
{
    public sealed class DriverSession
    {
        public Guid DriverId { get; private set; } = Guid.Empty;
        public string? DriverName { get; private set; }
        public string? Phone { get; private set; }
        public string? Email { get; private set; }
        public string? AccessToken { get; private set; }
        public DateTime? ExpiresAtUtc { get; private set; }

        public bool HasDriver => DriverId != Guid.Empty;
        public bool IsAuthenticated =>
            DriverId != Guid.Empty &&
            !string.IsNullOrWhiteSpace(AccessToken) &&
            ExpiresAtUtc.HasValue &&
            ExpiresAtUtc.Value > DateTime.UtcNow;

        public void SetDriver(
            Guid driverId,
            string? driverName = null,
            string? phone = null,
            string? email = null,
            string? accessToken = null,
            DateTime? expiresAtUtc = null)
        {
            DriverId = driverId;
            DriverName = driverName;
            Phone = phone;
            Email = email;
            AccessToken = accessToken;
            ExpiresAtUtc = expiresAtUtc;
        }

        public void Clear()
        {
            DriverId = Guid.Empty;
            DriverName = null;
            Phone = null;
            Email = null;
            AccessToken = null;
            ExpiresAtUtc = null;
        }
    }

}
