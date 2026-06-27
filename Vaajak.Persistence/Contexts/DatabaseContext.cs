using Microsoft.EntityFrameworkCore;
using Vaajak.Application.Interfaces.Context;
using Vaajak.Domain.Entities;

namespace Vaajak.Persistence.Contexts;

public class DatabaseContext: DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions) : base(dbContextOptions)
    {

    }

    public DbSet<Vocab> Vocabs { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<Example> Examples { get; set; }
    public DbSet<Translate> Translates { get; set; }
    public DbSet<UserPackage> UserPackages { get; set; }
    public DbSet<UserVocabProgress> UserVocabProgresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserPackage>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => new { e.UserId, e.PackageId }).IsUnique();

            entity.HasOne(e => e.Package)
                  .WithMany()
                  .HasForeignKey(e => e.PackageId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.EnrolledAt).IsRequired();
        });

        modelBuilder.Entity<UserVocabProgress>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.VocabId, e.PackageId }).IsUnique();
            entity.HasOne(e => e.Vocab)
                  .WithMany()
                  .HasForeignKey(e => e.VocabId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.UserId).IsRequired();
        });
    }
}
