using SmartParking.Application.Abstractions.Services;
using SmartParking.Domain.Common;

namespace SmartParking.Infrastructure.Services
{
    public sealed class BookingPricingService : IBookingPricingService
    {
        public int CalculateBillableHours(DateTime startTimeUtc, DateTime endTimeUtc)
        {
            if (endTimeUtc <= startTimeUtc)
                throw new DomainException("End time must be greater than start time.");

            var duration = endTimeUtc - startTimeUtc;
            var hours = Math.Ceiling(duration.TotalHours);

            return (int)hours;
        }

        public decimal CalculatePrice(decimal hourlyRate, DateTime startTimeUtc, DateTime endTimeUtc)
        {
            if (hourlyRate < 0)
                throw new DomainException("Hourly rate cannot be negative.");

            var billableHours = CalculateBillableHours(startTimeUtc, endTimeUtc);

            return hourlyRate * billableHours;
        }
    }
}
