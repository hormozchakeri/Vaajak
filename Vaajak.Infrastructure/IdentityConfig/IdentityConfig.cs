using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vaajak.Domain.Entities;
using Vaajak.Persistence.Contexts;

namespace Vaajak.Infrastructure.IdentityConfig
{
    public static class IdentityConfig
    {

        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("SqlServer");

            
            services.AddDbContext<IdentityDatabaseContext>(options =>options.UseSqlServer(connectionString));

            var builder = services.AddIdentityCore<User>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
            })
                .AddEntityFrameworkStores<IdentityDatabaseContext>()
                .AddRoles<Role>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();

            return services;

        }
    }
}

