namespace SmartParking.Mobile.Driver.Services
{
    public sealed class LocationService
    {
        public async Task<Location?> GetCurrentLocationAsync()
        {
            try
            {
                var request = new GeolocationRequest(
                    GeolocationAccuracy.Medium,
                    TimeSpan.FromSeconds(10));

                return await Geolocation.Default.GetLocationAsync(request);
            }
            catch
            {
                return null;
            }
        }

    }

}
