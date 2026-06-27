using Vaajak.Application.Dto.Practice;

namespace Vaajak.Application.Services.Practice;

public interface IPracticeService
{
    Task<List<PracticeCardDto>> GetSessionAsync(string userId, Guid packageId);
    Task<List<PracticeCardDto>> GetDailySessionAsync(string userId, int cardsPerPackage = 15);
    Task RateCardAsync(string userId, RateCardDto dto);
    Task<PackageProgressDto> GetProgressAsync(string userId, Guid packageId);
}
