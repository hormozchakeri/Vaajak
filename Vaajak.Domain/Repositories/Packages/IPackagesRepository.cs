using Vaajak.Domain.Entities;

namespace Vaajak.Domain.Repositories.Packages
{
    public interface IPackagesRepository
    {
        IQueryable<Package> GetAll();
        Task<Package?> GetPackageById(Guid id);
        Task<Package> CreatePackage(Package package);
        Task<List<Package>> GetByOwnerIdAsync(string ownerId);
    }
}