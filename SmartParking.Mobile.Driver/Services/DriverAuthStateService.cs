using SmartParking.Mobile.Driver.Models;

namespace SmartParking.Mobile.Driver.Services
{
    public sealed class DriverAuthStateService
    {
        private readonly DriverSession _driverSession;
        private readonly DriverPreferencesService _preferences;

        public DriverAuthStateService(
            DriverSession driverSession,
            DriverPreferencesService preferences)
        {
            _driverSession = driverSession;
            _preferences = preferences;
        }

        public bool IsAuthenticated => _driverSession.IsAuthenticated;

        public void SignIn(
            Guid driverId,
            string? driverName,
            string? phone,
            string? email,
            string accessToken,
            DateTime expiresAtUtc)
        {
            _driverSession.SetDriver(
                driverId,
                driverName,
                phone,
                email,
                accessToken,
                expiresAtUtc);

            _preferences.SaveSession(new DriverSessionData(
                driverId,
                driverName,
                phone,
                email,
                accessToken,
                expiresAtUtc));
        }

        public void ClearAuthenticationButKeepDriver()
        {
            _driverSession.SetDriver(
                _driverSession.DriverId,
                _driverSession.DriverName,
                _driverSession.Phone,
                _driverSession.Email,
                accessToken: null,
                expiresAtUtc: null);

            _preferences.SaveSession(new DriverSessionData(
                _driverSession.DriverId,
                _driverSession.DriverName,
                _driverSession.Phone,
                _driverSession.Email,
                AccessToken: null,
                ExpiresAtUtc: null));
        }

        public void SignOut()
        {
            _driverSession.Clear();
            _preferences.ClearSession();
        }
    }

}
