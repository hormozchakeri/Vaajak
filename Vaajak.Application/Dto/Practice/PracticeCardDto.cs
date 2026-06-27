namespace Vaajak.Application.Dto.Practice;

public class PracticeCardDto
{
    public Guid VocabId { get; set; }
    public Guid PackageId { get; set; }
    public string PackageName { get; set; } = string.Empty;
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public string? Usage { get; set; }
    public string? LastRating { get; set; }
    public int ReviewCount { get; set; }
    public DateTime? NextReviewAt { get; set; }
}
