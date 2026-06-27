using Vaajak.Application.Dto.Packages;

namespace Vaajak.Application.Dto.Vocabs
{
    public class VocabsDto
    {
        public Guid Id { get; set; }
        public string Vocabulary { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Voice { get; set; } = string.Empty;

        public string? IpaPronunciation { get; set; }
        public string? Meaning          { get; set; }
        public string? Example1         { get; set; }
        public string? Example2         { get; set; }
        public string? Example3         { get; set; }
        public string? Synonyms         { get; set; }
        public string? Antonyms         { get; set; }
        public string? WordFamily       { get; set; }
        public string? ImageFile        { get; set; }

        public List<PackageDto> Package { get; set; } = new();
    }
}