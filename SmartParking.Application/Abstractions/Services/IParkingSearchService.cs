using SmartParking.Contracts.Parkings;

namespace SmartParking.Application.Abstractions.Services
{
    public interface IParkingSearchService
    {
        Task<IReadOnlyList<ParkingSearchItemResponse>> SearchAsync(
            ParkingSearchRequest request,
            CancellationToken cancellationToken = default);
    }
}
