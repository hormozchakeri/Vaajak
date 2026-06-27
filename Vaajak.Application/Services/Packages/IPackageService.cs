using Vaajak.Application.Dto.Packages;
using Vaajak.Application.Dto.Primitives;

namespace Vaajak.Application.Services.Packages
{
    public interface IPackageService
    {
        Task<PaginatedResponse<PackageDto>> GetAllAsync(PaginationRequestDTO pagination);
        Task<PackageDto?> GetPackageById(Guid id);
        Task<PackageDto> CreatePackage(CreatePackageDto createPackageDto, string ownerId);
        Task<List<PackageDto>> GetMyPackagesAsync(string ownerId);
    }
}