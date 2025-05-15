using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vaajak.Domain.Entities;


namespace Vaajak.Persistence.Contexts
{
    public class IdentityDatabaseContext : IdentityDbContext<User, Role, Guid>
    {

        public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options) : base(options)
        {
        }
    }
}
