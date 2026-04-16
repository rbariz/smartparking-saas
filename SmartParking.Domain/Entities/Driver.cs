using SmartParking.Domain.Common;

namespace SmartParking.Domain.Entities
{
    public sealed class Driver : Entity
    {
        public string FullName { get; private set; } = default!;
        public string Phone { get; private set; } = default!;
        public string? Email { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }

        private Driver()
        {
        }

        public Driver(string fullName, string phone, string? email)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Phone = phone;
            Email = email;
            CreatedAtUtc = DateTime.UtcNow;
        }

        public void UpdateContact(string phone, string? email)
        {
            Phone = phone;
            Email = email;
        }


    }

}
