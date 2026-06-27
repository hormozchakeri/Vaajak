using System.Runtime;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Share;
using Vaajak.Domain.Entities;


namespace Vaajak.Persistence.Contexts
{
    public class IdentityDatabaseContext : IdentityDbContext<User, Role, Guid>
    {

        public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options) : base(options)
        {
            var connectionString = this.Database.GetDbConnection().ConnectionString;

            Console.WriteLine("Connection string is: " + connectionString);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(ConnectionStrings.IdentityDatabaseContext);
            optionsBuilder.UseNpgsql(ConnectionStrings.IdentityDatabaseContext);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
