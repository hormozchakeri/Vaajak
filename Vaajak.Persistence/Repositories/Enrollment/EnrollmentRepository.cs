using Microsoft.EntityFrameworkCore;
using Vaajak.Domain.Entities;
using Vaajak.Domain.Repositories.Enrollment;
using Vaajak.Persistence.Contexts;

namespace Vaajak.Persistence.Repositories.Enrollment;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly DatabaseContext _context;

    public EnrollmentRepository(DatabaseContext context)
    {
        _context = context;
    }

    public Task<bool> IsEnrolledAsync(string userId, Guid packageId)
        => _context.UserPackages
            .AnyAsync(up => up.UserId == userId && up.PackageId == packageId && up.Status == EnrollmentStatus.Active);

    public Task<IList<UserPackage>> GetUserEnrollmentsAsync(string userId)
        => _context.UserPackages
            .Include(up => up.Package)
            .Where(up => up.UserId == userId && up.Status == EnrollmentStatus.Active)
            .OrderByDescending(up => up.EnrolledAt)
            .ToListAsync()
            .ContinueWith(t => (IList<UserPackage>)t.Result);

    public async Task<UserPackage> EnrollAsync(UserPackage userPackage)
    {
        _context.UserPackages.Add(userPackage);
        await _context.SaveChangesAsync();
        return userPackage;
    }
}
