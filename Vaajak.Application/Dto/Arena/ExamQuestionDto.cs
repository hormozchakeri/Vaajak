namespace Vaajak.Application.Dto.Arena;

public class ExamQuestionDto
{
    public Guid VocabId { get; set; }
    public string Question { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new();
    public string CorrectAnswer { get; set; } = string.Empty;
}
