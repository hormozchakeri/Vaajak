using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Share;
using Vaajak.Application.Services.Account;
using Vaajak.Application.Services.Auth;
using Vaajak.Domain.Common.Auth;
using Vaajak.Domain.Entities;
using Vaajak.Domain.Repositories.Account;
using Vaajak.Domain.Repositories.Enrollment;
using Vaajak.Domain.Repositories.Packages;
using Vaajak.Domain.Repositories.Practice;
using Vaajak.Domain.Repositories.Vocabs;
using Vaajak.Persistence.Contexts;
using Vaajak.Persistence.Repositories.Account;
using Vaajak.Persistence.Repositories.Enrollment;
using Vaajak.Persistence.Repositories.Packages;
using Vaajak.Persistence.Repositories.Practice;
using Vaajak.Persistence.Repositories.Vocabs;

namespace Vaajak.Infrastructure.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(ConnectionStrings.DatabaseContext));
            services.AddDbContext<IdentityDatabaseContext>(options => options.UseNpgsql(ConnectionStrings.IdentityDatabaseContext));
            //var identityConnection = configuration.GetConnectionString("IdentityDatabaseContext");
            //var contextConnection = configuration.GetConnectionString("DatabaseContext");

            //services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(contextConnection));

            //services.AddDbContext<IdentityDatabaseContext>(options => options.UseSqlServer(identityConnection));

            //Console.WriteLine($"Using connection string: {ConnectionStrings}");
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<IdentityDatabaseContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IVocabsRepository, VocabRepository>();
            services.AddScoped<IPackagesRepository, PackageRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IPracticeRepository, PracticeRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();



        }
    }
}
