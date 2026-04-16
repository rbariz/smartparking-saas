using SmartParking.Operator.Models;

namespace SmartParking.Operator.Services
{
    
    public sealed class DashboardService
    {
        private readonly ParkingsAdminApiClient _parkingsApiClient;
        private readonly BookingsAdminApiClient _bookingsApiClient;
        private readonly PaymentsAdminApiClient _paymentsApiClient;

        public DashboardService(
            ParkingsAdminApiClient parkingsApiClient,
            BookingsAdminApiClient bookingsApiClient,
            PaymentsAdminApiClient paymentsApiClient)
        {
            _parkingsApiClient = parkingsApiClient;
            _bookingsApiClient = bookingsApiClient;
            _paymentsApiClient = paymentsApiClient;
        }

        public async Task<DashboardVm> GetAsync(CancellationToken cancellationToken = default)
        {
            var parkings = await _parkingsApiClient.GetAllAsync(cancellationToken);
            var bookings = await _bookingsApiClient.GetAllAsync(cancellationToken);
            var payments = await _paymentsApiClient.GetAllAsync(cancellationToken);

            return new DashboardVm
            {
                TotalParkings = parkings.Count,
                TotalCapacity = parkings.Sum(x => x.TotalCapacity),
                AvailableSpots = parkings.Sum(x => x.AvailableCount),

                ActiveBookings = bookings.Count(x => x.Status == "active"),
                CompletedBookings = bookings.Count(x => x.Status == "completed"),
                PendingPaymentBookings = bookings.Count(x => x.Status == "pending_payment"),

                ClosedParkings = parkings.Count(x => x.Status == "closed"),
                FullParkings = parkings.Count(x => x.AvailableCount == 0),
                OpenParkings = parkings.Count(x => x.Status == "open"),

                TotalPayments = payments.Count,
                PaidPayments = payments.Count(x => x.Status == "paid"),
                PendingPayments = payments.Count(x => x.Status == "pending"),

                TotalPaidAmount = payments
                    .Where(x => x.Status == "paid")
                    .Sum(x => x.Amount),

                TotalPendingAmount = payments
                    .Where(x => x.Status == "pending")
                    .Sum(x => x.Amount)
            };
        }
    }
}
