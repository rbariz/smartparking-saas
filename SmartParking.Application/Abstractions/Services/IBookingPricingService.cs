namespace SmartParking.Application.Abstractions.Services
{
    public interface IBookingPricingService
    {
        int CalculateBillableHours(DateTime startTimeUtc, DateTime endTimeUtc);
        decimal CalculatePrice(decimal hourlyRate, DateTime startTimeUtc, DateTime endTimeUtc);
    }
}
