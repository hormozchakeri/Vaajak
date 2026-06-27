using Vaajak.Application.Dto.Enrollment;

namespace Vaajak.Application.Services.Enrollment;

public interface IEnrollmentService
{
    Task<EnrollmentDto> EnrollAsync(string userId, Guid packageId);
    Task<IList<EnrollmentDto>> GetMyPackagesAsync(string userId);
    Task<bool> IsEnrolledAsync(string userId, Guid packageId);
}
