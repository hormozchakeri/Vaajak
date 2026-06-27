namespace Vaajak.Application.Dto.Practice;

public class RateCardDto
{
    public Guid VocabId { get; set; }
    public Guid PackageId { get; set; }
    public string Rating { get; set; } = string.Empty; // "Hard", "Good", "Easy"
}
