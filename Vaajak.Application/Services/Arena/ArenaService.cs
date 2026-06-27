using Vaajak.Application.Dto.Arena;
using Vaajak.Domain.Repositories.Practice;

namespace Vaajak.Application.Services.Arena;

public class ArenaService : IArenaService
{
    private readonly IPracticeRepository _practiceRepository;

    public ArenaService(IPracticeRepository practiceRepository)
    {
        _practiceRepository = practiceRepository;
    }

    public async Task<List<ExamQuestionDto>> GetQuestionsAsync(Guid packageId, int count = 25)
    {
        var vocabs = await _practiceRepository.GetPackageVocabsAsync(packageId);
        var vocabsWithTrans = vocabs.Where(v => v.Translations.Any()).ToList();

        if (vocabsWithTrans.Count < 4)
            throw new InvalidOperationException("Package needs at least 4 vocabs with translations for an exam.");

        var allTranslations = vocabsWithTrans
            .Select(v => v.Translations.First().Vocabtran)
            .ToList();

        var rng       = new Random();
        var questions = new List<ExamQuestionDto>();

        foreach (var vocab in vocabsWithTrans.OrderBy(_ => rng.Next()).Take(count))
        {
            var correct = vocab.Translations.First().Vocabtran;
            var wrongs  = allTranslations
                .Where(t => t != correct)
                .OrderBy(_ => rng.Next())
                .Take(3)
                .ToList();

            if (wrongs.Count < 3) continue;

            var options = wrongs.Append(correct).OrderBy(_ => rng.Next()).ToList();

            questions.Add(new ExamQuestionDto
            {
                VocabId       = vocab.Id,
                Question      = vocab.Vocabulary,
                CorrectAnswer = correct,
                Options       = options,
            });
        }

        return questions;
    }

    public async Task<ExamResultDto> SubmitExamAsync(ExamSubmitDto dto)
    {
        var vocabs    = await _practiceRepository.GetPackageVocabsAsync(dto.PackageId);
        var vocabMap  = vocabs.ToDictionary(v => v.Id);

        var correct = dto.Answers.Count(a =>
            vocabMap.TryGetValue(a.VocabId, out var v) &&
            v.Translations.FirstOrDefault()?.Vocabtran == a.SelectedAnswer);

        return new ExamResultDto
        {
            TotalQuestions  = dto.Answers.Count,
            CorrectAnswers  = correct,
            Score           = dto.Answers.Count == 0 ? 0
                              : (int)Math.Round((double)correct / dto.Answers.Count * 100),
        };
    }
}
