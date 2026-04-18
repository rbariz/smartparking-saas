using SmartParking.Domain.Common;

namespace SmartParking.Domain.Entities
{
    //public sealed class Driver : Entity
    //{
    //    public string FullName { get; private set; } = default!;
    //    public string Phone { get; private set; } = default!;
    //    public string? Email { get; private set; }
    //    public DateTime CreatedAtUtc { get; private set; }

    //    private Driver()
    //    {
    //    }

    //    public Driver(string fullName, string phone, string? email)
    //    {
    //        Id = Guid.NewGuid();
    //        FullName = fullName;
    //        Phone = phone;
    //        Email = email;
    //        CreatedAtUtc = DateTime.UtcNow;
    //    }

    //    public void UpdateContact(string phone, string? email)
    //    {
    //        Phone = phone;
    //        Email = email;
    //    }


    //}



    public sealed class Driver : Entity
    {
        public string FullName { get; private set; } = default!;
        public string Phone { get; private set; } = default!;
        public string? Email { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? LastLoginAtUtc { get; private set; }
        public bool IsActive { get; private set; }

        private Driver()
        {
        }

        public Driver(string fullName, string phone, string? email)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("Full name is required.");

            if (string.IsNullOrWhiteSpace(phone))
                throw new DomainException("Phone is required.");

            Id = Guid.NewGuid();
            FullName = fullName.Trim();
            Phone = phone.Trim();
            Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
            CreatedAtUtc = DateTime.UtcNow;
            IsActive = true;
        }

        public void UpdateContact(string phone, string? email)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new DomainException("Phone is required.");

            Phone = phone.Trim();
            Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
        }

        public void UpdateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("Full name is required.");

            FullName = fullName.Trim();
        }

        public void MarkLogin(DateTime nowUtc)
        {
            LastLoginAtUtc = nowUtc;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }

}
