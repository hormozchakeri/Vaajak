using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Share;
using Vaajak.Persistence.Contexts;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseNpgsql(ConnectionStrings.DatabaseContext);

        return new DatabaseContext(optionsBuilder.Options);
    }
}
