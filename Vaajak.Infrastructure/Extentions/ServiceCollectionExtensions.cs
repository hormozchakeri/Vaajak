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

namespace Vaajak.Infrastructure.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IVocabsRepository, VocabRepository>();
            services.AddScoped<IPackagesRepository, PackageRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        }
    }
}
