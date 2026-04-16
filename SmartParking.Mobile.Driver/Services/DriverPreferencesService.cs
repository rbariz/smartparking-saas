namespace SmartParking.Mobile.Driver.Services
{
    public sealed class DriverPreferencesService
    {
        private const string DriverIdKey = "driver_id";

        public Guid? GetDriverId()
        {
            var raw = Preferences.Default.Get(DriverIdKey, string.Empty);

            if (string.IsNullOrWhiteSpace(raw))
                return null;

            return Guid.TryParse(raw, out var driverId) ? driverId : null;
        }

        public void SaveDriverId(Guid driverId)
        {
            Preferences.Default.Set(DriverIdKey, driverId.ToString());
        }

        public void ClearDriverId()
        {
            Preferences.Default.Remove(DriverIdKey);
        }
    }


}
