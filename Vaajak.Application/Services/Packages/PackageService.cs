using Microsoft.EntityFrameworkCore;
using Vaajak.Application.Dto.Packages;
using Vaajak.Application.Dto.Primitives;
using Vaajak.Domain.Entities;
using Vaajak.Domain.Repositories.Packages;

namespace Vaajak.Application.Services.Packages
{
    public class PackageService : IPackageService
    {
        private readonly IPackagesRepository _packagesRepository;

        public PackageService(IPackagesRepository packagesRepository)
        {
            _packagesRepository = packagesRepository;
        }

        public async Task<PaginatedResponse<PackageDto>> GetAllAsync(PaginationRequestDTO pagination)
        {
            var pageNumber = pagination.PageNumber < 1 ? 1 : pagination.PageNumber;
            var pageSize = pagination.PageSize <= 0 ? 10 : pagination.PageSize;
            pageSize = pageSize > 50 ? 50 : pageSize;

            var query = _packagesRepository.GetAll();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PackageDto
                {
                    Id          = p.Id,
                    PackageName = p.PackageName,
                    Description = p.Description,
                    Price       = p.Price,
                    Producer    = p.Producer,
                    Rate        = p.Rate,
                    RateCount   = p.RateCount,
                })
                .ToListAsync();

            return new PaginatedResponse<PackageDto>
            {
                Items      = items,
                PageNumber = pageNumber,
                PageSize   = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<PackageDto?> GetPackageById(Guid id)
        {
            var p = await _packagesRepository.GetPackageById(id);

            if (p == null) return null;

            return new PackageDto
            {
                Id          = p.Id,
                PackageName = p.PackageName,
                Description = p.Description,
                Price       = p.Price,
                Producer    = p.Producer,
                Rate        = p.Rate,
                RateCount   = p.RateCount,
            };
        }

        public async Task<PackageDto> CreatePackage(CreatePackageDto createPackageDto, string ownerId)
        {
            var package = new Package
            {
                Id          = createPackageDto.Id != Guid.Empty ? createPackageDto.Id : Guid.NewGuid(),
                PackageName = createPackageDto.PackageName,
                Description = createPackageDto.Description,
                Price       = createPackageDto.Price,
                Producer    = createPackageDto.Producer,
                Rate        = createPackageDto.Rate,
                RateCount   = createPackageDto.RateCount,
                OwnerId     = ownerId,
            };

            var created = await _packagesRepository.CreatePackage(package);

            return new PackageDto
            {
                Id          = created.Id,
                PackageName = created.PackageName,
                Description = created.Description,
                Price       = created.Price,
                Producer    = created.Producer,
                Rate        = created.Rate,
                RateCount   = created.RateCount,
                OwnerId     = created.OwnerId,
                IsOwner     = true,
            };
        }

        public async Task<List<PackageDto>> GetMyPackagesAsync(string ownerId)
        {
            var packages = await _packagesRepository.GetAll()
                .Where(p => p.OwnerId == ownerId)
                .OrderByDescending(p => p.Id)
                .Select(p => new PackageDto
                {
                    Id          = p.Id,
                    PackageName = p.PackageName,
                    Description = p.Description,
                    Price       = p.Price,
                    Producer    = p.Producer,
                    Rate        = p.Rate,
                    RateCount   = p.RateCount,
                    OwnerId     = p.OwnerId,
                    IsOwner     = true,
                })
                .ToListAsync();

            return packages;
        }
    }
}
