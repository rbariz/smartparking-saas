using SmartParking.Application.Abstractions.Time;

namespace SmartParking.Infrastructure.Time
{
    public sealed class SystemClock : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
