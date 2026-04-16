using SmartParking.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking.Domain.Entities
{
    public sealed class Operator : Entity
    {
        public string Name { get; private set; } = default!;
        public string ContactEmail { get; private set; } = default!;
        public string ContactPhone { get; private set; } = default!;
        public DateTime CreatedAtUtc { get; private set; }

        private Operator()
        {
        }

        public Operator(string name, string contactEmail, string contactPhone)
        {
            Id = Guid.NewGuid();
            Name = name;
            ContactEmail = contactEmail;
            ContactPhone = contactPhone;
            CreatedAtUtc = DateTime.UtcNow;
        }

        public void UpdateContact(string contactEmail, string contactPhone)
        {
            ContactEmail = contactEmail;
            ContactPhone = contactPhone;
        }
    }

}
