namespace Vaajak.Application.Dto.Vocabs;

public class BulkImportVocabItemDto
{
    public string Word            { get; set; } = string.Empty;
    public string IpaPronunciation { get; set; } = string.Empty;
    public string Meaning  { get; set; } = string.Empty;
    public string Example1        { get; set; } = string.Empty;
    public string Example2        { get; set; } = string.Empty;
    public string Example3        { get; set; } = string.Empty;
    public string Synonyms        { get; set; } = string.Empty;
    public string Antonyms        { get; set; } = string.Empty;
    public string WordFamily      { get; set; } = string.Empty;
    public string ImageFile       { get; set; } = string.Empty;
    public string AudioFile       { get; set; } = string.Empty;
}

public class BulkImportVocabRequestDto
{
    public Guid PackageId { get; set; }
    public List<BulkImportVocabItemDto> Vocabs { get; set; } = [];
}

public class BulkImportVocabResultDto
{
    public int Imported { get; set; }
    public int Skipped  { get; set; }
    public List<string> Errors { get; set; } = [];
}
