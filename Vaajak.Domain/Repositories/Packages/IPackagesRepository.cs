using Vaajak.Domain.Entities;

namespace Vaajak.Domain.Repositories.Packages
{
    public interface IPackagesRepository
    {
        Task<IEnumerable<Package>> GetAllAsync();
        Task<Package> GetPackageById(Guid id);
        Task<Package> CreatePackage(Package package);
    }
}
