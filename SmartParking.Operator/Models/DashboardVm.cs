namespace SmartParking.Operator.Models
{
    public sealed class DashboardVm
    {
        public int TotalParkings { get; set; }
        public int TotalCapacity { get; set; }
        public int AvailableSpots { get; set; }

        public int OccupiedSpots => TotalCapacity - AvailableSpots;

        public decimal OccupancyRatePercent =>
            TotalCapacity == 0 ? 0 : Math.Round(((decimal)OccupiedSpots / TotalCapacity) * 100, 1);

        public int ActiveBookings { get; set; }
        public int CompletedBookings { get; set; }
        public int PendingPaymentBookings { get; set; }

        public int ClosedParkings { get; set; }
        public int FullParkings { get; set; }
        public int OpenParkings { get; set; }

        public int TotalPayments { get; set; }
        public int PaidPayments { get; set; }
        public int PendingPayments { get; set; }

        public decimal TotalPaidAmount { get; set; }
        public decimal TotalPendingAmount { get; set; }
        public bool HasFullParkings => FullParkings > 0;
        public bool HasPendingPaymentBookings => PendingPaymentBookings > 0;
        public bool HasPendingPayments => PendingPayments > 0;
        public bool HasClosedParkings => ClosedParkings > 0;
    }
}
