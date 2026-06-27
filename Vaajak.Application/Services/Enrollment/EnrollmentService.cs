using Vaajak.Application.Dto.Enrollment;
using Vaajak.Domain.Entities;
using Vaajak.Domain.Repositories.Enrollment;
using Vaajak.Domain.Repositories.Packages;
using Vaajak.Domain.Repositories.Practice;

namespace Vaajak.Application.Services.Enrollment;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IPackagesRepository _packagesRepository;
    private readonly IPracticeRepository _practiceRepository;

    public EnrollmentService(
        IEnrollmentRepository enrollmentRepository,
        IPackagesRepository packagesRepository,
        IPracticeRepository practiceRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _packagesRepository   = packagesRepository;
        _practiceRepository   = practiceRepository;
    }

    public async Task<EnrollmentDto> EnrollAsync(string userId, Guid packageId)
    {
        var alreadyEnrolled = await _enrollmentRepository.IsEnrolledAsync(userId, packageId);
        if (alreadyEnrolled)
            throw new InvalidOperationException("User is already enrolled in this package.");

        var package = await _packagesRepository.GetPackageById(packageId)
            ?? throw new KeyNotFoundException("Package not found.");

        var userPackage = new UserPackage
        {
            Id         = Guid.NewGuid(),
            UserId     = userId,
            PackageId  = packageId,
            EnrolledAt = DateTime.UtcNow,
            Status     = EnrollmentStatus.Active,
        };

        var created = await _enrollmentRepository.EnrollAsync(userPackage);

        return new EnrollmentDto
        {
            Id          = created.Id,
            PackageId   = created.PackageId,
            PackageName = package.PackageName,
            EnrolledAt  = created.EnrolledAt,
            Status      = created.Status,
        };
    }

    public async Task<IList<EnrollmentDto>> GetMyPackagesAsync(string userId)
    {
        var enrollments = await _enrollmentRepository.GetUserEnrollmentsAsync(userId);
        var result = new List<EnrollmentDto>();

        foreach (var e in enrollments)
        {
            int total = 0, reviewed = 0;
            try
            {
                total    = await _practiceRepository.GetVocabCountForPackageAsync(e.PackageId);
                reviewed = await _practiceRepository.GetReviewedCountForPackageAsync(userId, e.PackageId);
            }
            catch { /* progress data not critical — return enrollment without it */ }

            result.Add(new EnrollmentDto
            {
                Id              = e.Id,
                PackageId       = e.PackageId,
                PackageName     = e.Package.PackageName,
                EnrolledAt      = e.EnrolledAt,
                Status          = e.Status,
                TotalCards      = total,
                ReviewedCards   = reviewed,
                ProgressPercent = total == 0 ? 0 : (int)Math.Round((double)reviewed / total * 100),
            });
        }

        return result;
    }

    public Task<bool> IsEnrolledAsync(string userId, Guid packageId)
        => _enrollmentRepository.IsEnrolledAsync(userId, packageId);
}
