using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Vaajak.Domain.Common.Auth;
using Vaajak.Domain.Repositories.Packages;
using Vaajak.Domain.Repositories.Vocabs;
using Vaajak.Persistence.Repositories.Packages;
using Vaajak.Persistence.Repositories.Vocabs;
using Vaajak.Application.Services.Auth;
using Vaajak.Persistence.Contexts;
using Share;
using Vaajak.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Vaajak.Infrastructure.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(ConnectionStrings.DatabaseContext));
            services.AddDbContext<IdentityDatabaseContext>(options => options.UseSqlServer(ConnectionStrings.IdentityDatabaseContext));
            //var identityConnection = configuration.GetConnectionString("IdentityDatabaseContext");
            //var contextConnection = configuration.GetConnectionString("DatabaseContext");

            //services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(contextConnection));

            //services.AddDbContext<IdentityDatabaseContext>(options => options.UseSqlServer(identityConnection));

            Console.WriteLine($"Using connection string: {ConnectionStrings.IdentityDatabaseContext}");
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<IdentityDatabaseContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IVocabsRepository, VocabRepository>();
            services.AddScoped<IPackagesRepository, PackageRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        }
    }
}
