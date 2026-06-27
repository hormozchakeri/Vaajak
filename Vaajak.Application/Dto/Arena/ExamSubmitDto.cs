namespace Vaajak.Application.Dto.Arena;

public class ExamAnswerDto
{
    public Guid VocabId { get; set; }
    public string SelectedAnswer { get; set; } = string.Empty;
}

public class ExamSubmitDto
{
    public Guid PackageId { get; set; }
    public List<ExamAnswerDto> Answers { get; set; } = new();
}
