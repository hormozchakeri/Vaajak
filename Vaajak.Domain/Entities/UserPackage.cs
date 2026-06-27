namespace Vaajak.Domain.Entities;

public class UserPackage
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public Guid PackageId { get; set; }
    public DateTime EnrolledAt { get; set; }
    public EnrollmentStatus Status { get; set; }

    public Package Package { get; set; } = null!;
}
