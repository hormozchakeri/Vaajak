namespace Vaajak.Application.Dto.Packages
{
    public class CreatePackageDto
    {
        public Guid Id { get; set; }
        public string PackageName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Producer { get; set; }
        public double? Rate { get; set; }
        public int? RateCount { get; set; }
    }
}
