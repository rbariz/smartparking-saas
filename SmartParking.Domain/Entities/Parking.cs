using SmartParking.Domain.Common;
using SmartParking.Domain.Enums;

namespace SmartParking.Domain.Entities
{
    public sealed class Parking : Entity
    {
        public Guid OperatorId { get; private set; }
        public string Name { get; private set; } = default!;
        public string Address { get; private set; } = default!;
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }
        public int TotalCapacity { get; private set; }
        public int AvailableCount { get; private set; }
        public ParkingStatus Status { get; private set; }
        public string Currency { get; private set; } = default!;
        public decimal HourlyRate { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime UpdatedAtUtc { get; private set; }

        private Parking()
        {
        }

        public Parking(
            Guid operatorId,
            string name,
            string address,
            decimal latitude,
            decimal longitude,
            int totalCapacity,
            string currency,
            decimal hourlyRate)
        {
            Id = Guid.NewGuid();
            OperatorId = operatorId;
            Name = name;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
            TotalCapacity = totalCapacity;
            AvailableCount = totalCapacity;
            Status = ParkingStatus.Closed;
            Currency = currency;
            HourlyRate = hourlyRate;
            CreatedAtUtc = DateTime.UtcNow;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public bool IsOpen => Status == ParkingStatus.Open;

        public void Open(int availableCount)
        {
            if (availableCount < 0 || availableCount > TotalCapacity)
                throw new DomainException("Available count is invalid.");

            AvailableCount = availableCount;
            Status = ParkingStatus.Open;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public void Close()
        {
            Status = ParkingStatus.Closed;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public void UpdateAvailability(int availableCount)
        {
            if (availableCount < 0 || availableCount > TotalCapacity)
                throw new DomainException("Available count is invalid.");

            AvailableCount = availableCount;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public void ReserveCapacity(int quantity = 1)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be positive.");

            if (AvailableCount < quantity)
                throw new DomainException("Not enough parking availability.");

            AvailableCount -= quantity;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public void ReleaseCapacity(int quantity = 1)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be positive.");

            var next = AvailableCount + quantity;
            if (next > TotalCapacity)
                throw new DomainException("Available count cannot exceed total capacity.");

            AvailableCount = next;
            UpdatedAtUtc = DateTime.UtcNow;
        }
    }

}
