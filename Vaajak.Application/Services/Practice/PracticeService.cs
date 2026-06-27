using Vaajak.Application.Dto.Practice;
using Vaajak.Domain.Entities;
using Vaajak.Domain.Repositories.Enrollment;
using Vaajak.Domain.Repositories.Packages;
using Vaajak.Domain.Repositories.Practice;

namespace Vaajak.Application.Services.Practice;

public class PracticeService : IPracticeService
{
    private readonly IPracticeRepository   _practiceRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IPackagesRepository   _packageRepository;

    public PracticeService(
        IPracticeRepository practiceRepository,
        IEnrollmentRepository enrollmentRepository,
        IPackagesRepository packageRepository)
    {
        _practiceRepository   = practiceRepository;
        _enrollmentRepository = enrollmentRepository;
        _packageRepository    = packageRepository;
    }

    // ── Public interface ────────────────────────────────────────────────────

    public async Task<List<PracticeCardDto>> GetSessionAsync(string userId, Guid packageId)
    {
        var vocabs      = await _practiceRepository.GetPackageVocabsAsync(packageId);
        var progressMap = (await _practiceRepository.GetUserProgressForPackageAsync(userId, packageId))
                          .ToDictionary(p => p.VocabId);

        var now = DateTime.UtcNow;

        // Due = no record yet (new) OR NextReviewAt <= now
        var due = vocabs
            .Where(v => !progressMap.ContainsKey(v.Id) || progressMap[v.Id].NextReviewAt <= now)
            .OrderBy(v => progressMap.TryGetValue(v.Id, out var p) ? (p.NextReviewAt ?? DateTime.MinValue) : DateTime.MinValue)
            .Take(20)
            .Select(v => ToCardDto(v, packageId, string.Empty, progressMap))
            .ToList();

        return due;
    }

    public async Task<List<PracticeCardDto>> GetDailySessionAsync(string userId, int cardsPerPackage = 15)
    {
        var maxDailyCards = cardsPerPackage * 4;
        var allCards = new List<PracticeCardDto>();

        // Enrolled packages
        var enrollments = await _enrollmentRepository.GetUserEnrollmentsAsync(userId);
        foreach (var enrollment in enrollments)
        {
            var cards = await GetSessionAsync(userId, enrollment.PackageId);
            var slice = cards.Take(cardsPerPackage).ToList();
            foreach (var c in slice)
                c.PackageName = enrollment.Package.PackageName;
            allCards.AddRange(slice);
        }

        // Owned packages (not already covered by an enrollment)
        var enrolledIds = enrollments.Select(e => e.PackageId).ToHashSet();
        var ownedPackages = await _packageRepository.GetByOwnerIdAsync(userId);
        foreach (var pkg in ownedPackages.Where(p => !enrolledIds.Contains(p.Id)))
        {
            var cards = await GetSessionAsync(userId, pkg.Id);
            var slice = cards.Take(cardsPerPackage).ToList();
            foreach (var c in slice)
                c.PackageName = pkg.PackageName;
            allCards.AddRange(slice);
        }

        var rng = new Random();
        return allCards.OrderBy(_ => rng.Next()).Take(maxDailyCards).ToList();
    }

    public async Task RateCardAsync(string userId, RateCardDto dto)
    {
        var existing = await _practiceRepository.GetSingleProgressAsync(userId, dto.VocabId, dto.PackageId);

        if (existing is null)
        {
            var (interval, repetitions, easeFactor, nextReview) = Sm2(0, 0, 2.5, dto.Rating);
            await _practiceRepository.AddProgressAsync(new UserVocabProgress
            {
                Id             = Guid.NewGuid(),
                UserId         = userId,
                VocabId        = dto.VocabId,
                PackageId      = dto.PackageId,
                LastRating     = dto.Rating,
                ReviewCount    = 1,
                LastReviewedAt = DateTime.UtcNow,
                Interval       = interval,
                Repetitions    = repetitions,
                EaseFactor     = easeFactor,
                NextReviewAt   = nextReview,
            });
        }
        else
        {
            var (interval, repetitions, easeFactor, nextReview) =
                Sm2(existing.Interval, existing.Repetitions, existing.EaseFactor, dto.Rating);

            existing.LastRating     = dto.Rating;
            existing.ReviewCount   += 1;
            existing.LastReviewedAt = DateTime.UtcNow;
            existing.Interval       = interval;
            existing.Repetitions    = repetitions;
            existing.EaseFactor     = easeFactor;
            existing.NextReviewAt   = nextReview;

            await _practiceRepository.UpdateProgressAsync(existing);
        }
    }

    public async Task<PackageProgressDto> GetProgressAsync(string userId, Guid packageId)
    {
        var total    = await _practiceRepository.GetVocabCountForPackageAsync(packageId);
        var reviewed = await _practiceRepository.GetReviewedCountForPackageAsync(userId, packageId);

        return new PackageProgressDto
        {
            PackageId       = packageId,
            TotalCards      = total,
            ReviewedCards   = reviewed,
            ProgressPercent = total == 0 ? 0 : (int)Math.Round((double)reviewed / total * 100),
        };
    }

    // ── SM-2 algorithm ──────────────────────────────────────────────────────
    //
    //  Hard  (q=1): reset to 1-day interval, decrease ease
    //  Good  (q=3): advance normally  → 1d → 3d → interval*EF
    //  Easy  (q=5): advance + boost   → skip a step, increase ease
    //
    private static (int interval, int repetitions, double easeFactor, DateTime nextReview)
        Sm2(int currentInterval, int repetitions, double easeFactor, string rating)
    {
        int quality = rating switch
        {
            "Easy" => 5,
            "Good" => 3,
            _      => 1,   // Hard
        };

        double newEF = easeFactor;
        int newInterval;
        int newRepetitions;

        if (quality < 3) // Hard — reset
        {
            newInterval    = 1;
            newRepetitions = 0;
            newEF          = Math.Max(1.3, easeFactor - 0.2);
        }
        else
        {
            newRepetitions = repetitions + 1;

            newInterval = repetitions switch
            {
                0 => 1,
                1 => 3,
                _ => (int)Math.Round(currentInterval * easeFactor),
            };

            if (quality == 5) // Easy — extra boost
            {
                newInterval = (int)Math.Round(newInterval * 1.3);
                newEF       = Math.Min(4.0, easeFactor + 0.15);
            }

            // Guard against getting stuck
            if (newInterval < 1) newInterval = 1;
        }

        var nextReview = DateTime.UtcNow.AddDays(newInterval);
        return (newInterval, newRepetitions, newEF, nextReview);
    }

    // ── Helpers ─────────────────────────────────────────────────────────────

    private static PracticeCardDto ToCardDto(
        Vocab vocab,
        Guid packageId,
        string packageName,
        Dictionary<Guid, UserVocabProgress> progressMap)
    {
        var firstTrans = vocab.Translations.FirstOrDefault();
        progressMap.TryGetValue(vocab.Id, out var prog);

        return new PracticeCardDto
        {
            VocabId      = vocab.Id,
            PackageId    = packageId,
            PackageName  = packageName,
            Question     = vocab.Vocabulary,
            Answer       = vocab.Meaning ?? firstTrans?.Vocabtran ?? string.Empty,
            Usage        = vocab.Example1 ?? firstTrans?.Usage,
            LastRating   = prog?.LastRating,
            ReviewCount  = prog?.ReviewCount ?? 0,
            NextReviewAt = prog?.NextReviewAt,
        };
    }
}
