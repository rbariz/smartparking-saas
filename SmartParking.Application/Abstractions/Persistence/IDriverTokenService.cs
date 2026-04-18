using SmartParking.Contracts.DriverAuth;
using SmartParking.Domain.Entities;

namespace SmartParking.Application.Abstractions.Persistence
{
    public interface IDriverTokenService
    {
        DriverTokenResult Generate(Driver driver);
    }
}
