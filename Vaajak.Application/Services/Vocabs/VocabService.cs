using Vaajak.Application.Dto.Primitives;
using Vaajak.Application.Dto.Vocabs;
using Vaajak.Domain.Entities;
using Vaajak.Domain.Repositories.Vocabs;
using X.PagedList;
using X.PagedList.Extensions;

namespace Vaajak.Application.Services.Vocabs
{
    public class VocabService : IVocabService
    {
        private readonly IVocabsRepository _vocabsRepository;

        public VocabService(IVocabsRepository vocabsRepository)
        {
            _vocabsRepository = vocabsRepository;
        }

        public async Task<IPagedList<VocabsDto>> GetAllAsync(Guid packageId, PaginationRequestDTO pagination)
        {
            var vocabs = await _vocabsRepository.GetAllAsync();
            var dtos = vocabs.Select(ToDto);
            return dtos.ToPagedList(pagination.PageNumber, pagination.PageSize);
        }

        public async Task<VocabsDto?> GetById(Guid id)
        {
            var vocab = await _vocabsRepository.GetByIdAsync(id);
            return vocab == null ? null : ToDto(vocab);
        }

        public async Task<CreateVocabDto> CreateVocab(CreateVocabDto dto)
        {
            var vocab = new Vocab
            {
                Id               = dto.Id.HasValue && dto.Id.Value != Guid.Empty ? dto.Id.Value : Guid.NewGuid(),
                Vocabulary       = dto.Vocabulary,
                Type             = dto.Type,
                Voice            = dto.Voice,
                IpaPronunciation = dto.IpaPronunciation,
                Meaning   = dto.Meaning,
                Example1         = dto.Example1,
                Example2         = dto.Example2,
                Example3         = dto.Example3,
                Synonyms         = dto.Synonyms,
                Antonyms         = dto.Antonyms,
                WordFamily       = dto.WordFamily,
                ImageFile        = dto.ImageFile,
            };

            var created = await _vocabsRepository.CreateVocab(vocab);

            return new CreateVocabDto
            {
                Vocabulary       = created!.Vocabulary,
                Type             = created.Type,
                Voice            = created.Voice,
                IpaPronunciation = created.IpaPronunciation,
                Meaning   = created.Meaning,
                Example1         = created.Example1,
                Example2         = created.Example2,
                Example3         = created.Example3,
                Synonyms         = created.Synonyms,
                Antonyms         = created.Antonyms,
                WordFamily       = created.WordFamily,
                ImageFile        = created.ImageFile,
            };
        }

        public async Task<UpdateVocabDto?> UpdateVocab(UpdateVocabDto dto)
        {
            var vocab = new Vocab
            {
                Id               = dto.Id,
                Vocabulary       = dto.Vocabulary,
                Type             = dto.Type,
                Voice            = dto.Voice,
                IpaPronunciation = dto.IpaPronunciation,
                Meaning   = dto.Meaning,
                Example1         = dto.Example1,
                Example2         = dto.Example2,
                Example3         = dto.Example3,
                Synonyms         = dto.Synonyms,
                Antonyms         = dto.Antonyms,
                WordFamily       = dto.WordFamily,
                ImageFile        = dto.ImageFile,
            };

            var updated = await _vocabsRepository.UpdateVocab(vocab);
            if (updated == null) return null;

            return new UpdateVocabDto
            {
                Id               = updated.Id,
                Vocabulary       = updated.Vocabulary,
                Type             = updated.Type,
                Voice            = updated.Voice,
                IpaPronunciation = updated.IpaPronunciation,
                Meaning   = updated.Meaning,
                Example1         = updated.Example1,
                Example2         = updated.Example2,
                Example3         = updated.Example3,
                Synonyms         = updated.Synonyms,
                Antonyms         = updated.Antonyms,
                WordFamily       = updated.WordFamily,
                ImageFile        = updated.ImageFile,
            };
        }

        public async Task<bool> DeleteById(Guid id)
            => await _vocabsRepository.DeleteVocab(id);

        public async Task<BulkImportVocabResultDto> BulkImportAsync(BulkImportVocabRequestDto request)
        {
            var result = new BulkImportVocabResultDto();

            var vocabs = request.Vocabs
                .Where(item => !string.IsNullOrWhiteSpace(item.Word))
                .Select(item => new Vocab
                {
                    Vocabulary       = item.Word.Trim(),
                    Voice            = item.AudioFile.Trim(),
                    IpaPronunciation = item.IpaPronunciation.Trim(),
                    Meaning   = item.Meaning.Trim(),
                    Example1         = item.Example1.Trim(),
                    Example2         = item.Example2.Trim(),
                    Example3         = item.Example3.Trim(),
                    Synonyms         = item.Synonyms.Trim(),
                    Antonyms         = item.Antonyms.Trim(),
                    WordFamily       = item.WordFamily.Trim(),
                    ImageFile        = item.ImageFile.Trim(),
                })
                .ToList();

            try
            {
                (result.Imported, result.Skipped) =
                    await _vocabsRepository.BulkCreateAsync(vocabs, request.PackageId);
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        // ── Helpers ──────────────────────────────────────────────────────────

        private static VocabsDto ToDto(Vocab v) => new()
        {
            Id               = v.Id,
            Vocabulary       = v.Vocabulary,
            Type             = v.Type,
            Voice            = v.Voice,
            IpaPronunciation = v.IpaPronunciation,
            Meaning   = v.Meaning,
            Example1         = v.Example1,
            Example2         = v.Example2,
            Example3         = v.Example3,
            Synonyms         = v.Synonyms,
            Antonyms         = v.Antonyms,
            WordFamily       = v.WordFamily,
            ImageFile        = v.ImageFile,
        };
    }
}
