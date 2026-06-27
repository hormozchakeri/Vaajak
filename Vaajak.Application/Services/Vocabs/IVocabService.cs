using Vaajak.Application.Dto.Primitives;
using Vaajak.Application.Dto.Vocabs;
using Vaajak.Domain.Entities;
using X.PagedList;

namespace Vaajak.Application.Services.Vocabs
{
    public interface IVocabService
    {
        Task<IPagedList<VocabsDto>> GetAllAsync(Guid packageId, PaginationRequestDTO pagination);
        Task<VocabsDto?> GetById(Guid id);
        Task<CreateVocabDto> CreateVocab(CreateVocabDto createVocabDto);
        Task<UpdateVocabDto> UpdateVocab(UpdateVocabDto updateVocabDto);
        Task<bool> DeleteById(Guid id);
        Task<BulkImportVocabResultDto> BulkImportAsync(BulkImportVocabRequestDto request);
    }
}