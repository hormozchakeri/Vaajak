namespace Vaajak.Domain.Entities;

public class Vocab
{
    public Guid Id { get; set; }
    public string Vocabulary { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Voice { get; set; } = string.Empty;

    // CSV-imported fields
    public string? IpaPronunciation { get; set; }
    public string? Meaning          { get; set; }
    public string? Example1         { get; set; }
    public string? Example2         { get; set; }
    public string? Example3         { get; set; }
    public string? Synonyms         { get; set; }
    public string? Antonyms         { get; set; }
    public string? WordFamily       { get; set; }
    public string? ImageFile        { get; set; }

    public ICollection<Package> Package { get; set; } = new List<Package>();
    public ICollection<Translate> Translations { get; set; } = new List<Translate>();
}
