using SmartParking.Domain.Common;
using SmartParking.Domain.Enums;

namespace SmartParking.Domain.Entities
{
    public sealed class Booking : Entity
    {
        public Guid DriverId { get; private set; }
        public Guid ParkingId { get; private set; }
        public DateTime StartTimeUtc { get; private set; }
        public DateTime EndTimeUtc { get; private set; }
        public BookingStatus Status { get; private set; }
        public decimal ReservedPrice { get; private set; }
        public string PriceCurrency { get; private set; } = default!;
        public DateTime ExpiresAtUtc { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime UpdatedAtUtc { get; private set; }

        private Booking()
        {
        }

        public Booking(
            Guid driverId,
            Guid parkingId,
            DateTime startTimeUtc,
            DateTime endTimeUtc,
            decimal reservedPrice,
            string priceCurrency,
            DateTime expiresAtUtc)
        {
            if (endTimeUtc <= startTimeUtc)
                throw new DomainException("Booking end time must be greater than start time.");

            Id = Guid.NewGuid();
            DriverId = driverId;
            ParkingId = parkingId;
            StartTimeUtc = startTimeUtc;
            EndTimeUtc = endTimeUtc;
            Status = BookingStatus.PendingPayment;
            ReservedPrice = reservedPrice;
            PriceCurrency = priceCurrency;
            ExpiresAtUtc = expiresAtUtc;
            CreatedAtUtc = DateTime.UtcNow;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public bool IsPendingPayment => Status == BookingStatus.PendingPayment;
        public bool IsConfirmed => Status == BookingStatus.Confirmed;

        public void Confirm()
        {
            if (Status != BookingStatus.PendingPayment)
                throw new DomainException("Only pending payment bookings can be confirmed.");

            Status = BookingStatus.Confirmed;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public void Expire(DateTime nowUtc)
        {
            if (Status != BookingStatus.PendingPayment)
                throw new DomainException("Only pending payment bookings can expire.");

            if (nowUtc < ExpiresAtUtc)
                throw new DomainException("Booking cannot expire before its expiration time.");

            Status = BookingStatus.Expired;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        //public void Cancel()
        //{
        //    if (Status != BookingStatus.PendingPayment && Status != BookingStatus.Confirmed)
        //        throw new DomainException("Only pending or confirmed bookings can be cancelled.");

        //    Status = BookingStatus.Cancelled;
        //    UpdatedAtUtc = DateTime.UtcNow;
        //}
        public void MarkActive(DateTime nowUtc)
        {
            if (Status != BookingStatus.Confirmed)
                throw new DomainException("Only confirmed bookings can become active.");

            if (nowUtc < StartTimeUtc)
                throw new DomainException("Booking cannot become active before its start time.");

            Status = BookingStatus.Active;
            UpdatedAtUtc = nowUtc;
        }

        public void MarkCompleted(DateTime nowUtc)
        {
            if (Status != BookingStatus.Active && Status != BookingStatus.Confirmed)
                throw new DomainException("Only active or confirmed bookings can be completed.");

            if (nowUtc < EndTimeUtc)
                throw new DomainException("Booking cannot be completed before its end time.");

            Status = BookingStatus.Completed;
            UpdatedAtUtc = nowUtc;
        }
        public void Cancel(DateTime nowUtc)
        {
            if (Status == BookingStatus.Completed)
                throw new DomainException("Completed bookings cannot be cancelled.");

            if (Status == BookingStatus.Cancelled)
                throw new DomainException("Booking is already cancelled.");

            if (Status == BookingStatus.Expired)
                throw new DomainException("Expired bookings cannot be cancelled.");

            Status = BookingStatus.Cancelled;
            UpdatedAtUtc = nowUtc;
        }


    }
    
    }
