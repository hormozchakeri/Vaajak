using Vaajak.Domain.Entities;

namespace Vaajak.Application.Dto.Enrollment;

public class EnrollmentDto
{
    public Guid Id { get; set; }
    public Guid PackageId { get; set; }
    public string PackageName { get; set; } = string.Empty;
    public DateTime EnrolledAt { get; set; }
    public EnrollmentStatus Status { get; set; }
    public int TotalCards { get; set; }
    public int ReviewedCards { get; set; }
    public int ProgressPercent { get; set; }
}
