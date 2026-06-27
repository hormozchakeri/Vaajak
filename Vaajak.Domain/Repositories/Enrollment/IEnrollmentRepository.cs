using Vaajak.Domain.Entities;

namespace Vaajak.Domain.Repositories.Enrollment;

public interface IEnrollmentRepository
{
    Task<bool> IsEnrolledAsync(string userId, Guid packageId);
    Task<IList<UserPackage>> GetUserEnrollmentsAsync(string userId);
    Task<UserPackage> EnrollAsync(UserPackage userPackage);
}
