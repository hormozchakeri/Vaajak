using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Vaajak.Persistence.Contexts;
using Share;

namespace Vaajak.Persistence.Factories
{
    public class IdentityDatabaseContextFactory : IDesignTimeDbContextFactory<IdentityDatabaseContext>
    {
        public IdentityDatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityDatabaseContext>();
            optionsBuilder.UseNpgsql(ConnectionStrings.IdentityDatabaseContext);

            return new IdentityDatabaseContext(optionsBuilder.Options);
        }
    }
}
