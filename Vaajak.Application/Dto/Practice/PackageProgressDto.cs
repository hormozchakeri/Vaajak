namespace Vaajak.Application.Dto.Practice;

public class PackageProgressDto
{
    public Guid PackageId { get; set; }
    public int TotalCards { get; set; }
    public int ReviewedCards { get; set; }
    public int ProgressPercent { get; set; }
}
