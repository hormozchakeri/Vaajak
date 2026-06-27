using Vaajak.Domain.Entities;

namespace Vaajak.Domain.Repositories.Practice;

public interface IPracticeRepository
{
    Task<List<Vocab>> GetPackageVocabsAsync(Guid packageId);
    Task<List<UserVocabProgress>> GetUserProgressForPackageAsync(string userId, Guid packageId);
    Task<UserVocabProgress?> GetSingleProgressAsync(string userId, Guid vocabId, Guid packageId);
    Task AddProgressAsync(UserVocabProgress progress);
    Task UpdateProgressAsync(UserVocabProgress progress);
    Task<int> GetVocabCountForPackageAsync(Guid packageId);
    Task<int> GetReviewedCountForPackageAsync(string userId, Guid packageId);
}
