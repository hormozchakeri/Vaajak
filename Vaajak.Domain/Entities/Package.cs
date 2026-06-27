namespace Vaajak.Domain.Entities;

public class Package
{
    public Guid Id { get; set; }
    public string PackageName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? Producer { get; set; }
    public double? Rate { get; set; }
    public int? RateCount { get; set; }
    public string? OwnerId { get; set; }
    public ICollection<Vocab> Vocabs { get; set; } = new List<Vocab>();
}
