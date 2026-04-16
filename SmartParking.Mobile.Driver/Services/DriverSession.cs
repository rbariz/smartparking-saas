namespace SmartParking.Mobile.Driver.Services
{
    public sealed class DriverSession
    {
        public Guid DriverId { get; private set; } = Guid.Empty;
        public string? DriverName { get; private set; }

        public bool HasDriver => DriverId != Guid.Empty;

        public void SetDriver(Guid driverId, string? driverName = null)
        {
            DriverId = driverId;
            DriverName = driverName;
        }

        public void Clear()
        {
            DriverId = Guid.Empty;
            DriverName = null;
        }
    }


}
