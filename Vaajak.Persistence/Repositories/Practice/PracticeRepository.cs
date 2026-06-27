using Microsoft.EntityFrameworkCore;
using Vaajak.Domain.Entities;
using Vaajak.Domain.Repositories.Practice;
using Vaajak.Persistence.Contexts;

namespace Vaajak.Persistence.Repositories.Practice;

public class PracticeRepository : IPracticeRepository
{
    private readonly DatabaseContext _context;

    public PracticeRepository(DatabaseContext context)
    {
        _context = context;
    }

    public Task<List<Vocab>> GetPackageVocabsAsync(Guid packageId)
        => _context.Vocabs
            .AsNoTracking()
            .Include(v => v.Translations)
            .Where(v => v.Package.Any(p => p.Id == packageId))
            .ToListAsync();

    public Task<List<UserVocabProgress>> GetUserProgressForPackageAsync(string userId, Guid packageId)
        => _context.UserVocabProgresses
            .AsNoTracking()
            .Where(p => p.UserId == userId && p.PackageId == packageId)
            .ToListAsync();

    public Task<UserVocabProgress?> GetSingleProgressAsync(string userId, Guid vocabId, Guid packageId)
        => _context.UserVocabProgresses
            .FirstOrDefaultAsync(p => p.UserId == userId && p.VocabId == vocabId && p.PackageId == packageId);

    public async Task AddProgressAsync(UserVocabProgress progress)
    {
        _context.UserVocabProgresses.Add(progress);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProgressAsync(UserVocabProgress progress)
    {
        _context.UserVocabProgresses.Update(progress);
        await _context.SaveChangesAsync();
    }

    public Task<int> GetVocabCountForPackageAsync(Guid packageId)
        => _context.Vocabs
            .CountAsync(v => v.Package.Any(p => p.Id == packageId));

    public Task<int> GetReviewedCountForPackageAsync(string userId, Guid packageId)
        => _context.UserVocabProgresses
            .CountAsync(p => p.UserId == userId
                          && p.PackageId == packageId
                          && p.Repetitions > 0
                          && p.LastRating != "Hard");
}
