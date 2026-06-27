using Vaajak.Application.Dto.Arena;

namespace Vaajak.Application.Services.Arena;

public interface IArenaService
{
    Task<List<ExamQuestionDto>> GetQuestionsAsync(Guid packageId, int count = 25);
    Task<ExamResultDto> SubmitExamAsync(ExamSubmitDto dto);
}
