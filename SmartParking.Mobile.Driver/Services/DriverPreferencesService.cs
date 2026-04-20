using SmartParking.Mobile.Driver.Models;
using System.Text.Json;

namespace SmartParking.Mobile.Driver.Services
{
    public sealed class DriverPreferencesService
    {
        private const string DriverSessionKey = "driver_session";

        public DriverSessionData? GetSession()
        {
            var raw = Preferences.Default.Get(DriverSessionKey, string.Empty);

            if (string.IsNullOrWhiteSpace(raw))
                return null;

            try
            {
                return JsonSerializer.Deserialize<DriverSessionData>(raw);
            }
            catch
            {
                return null;
            }
        }

        public void SaveSession(DriverSessionData session)
        {
            var raw = JsonSerializer.Serialize(session);
            Preferences.Default.Set(DriverSessionKey, raw);
        }

        public void ClearSession()
        {
            Preferences.Default.Remove(DriverSessionKey);
        }
    }

    

}
