using Microsoft.Extensions.DependencyInjection;
using Vaajak.Application.Services.Arena;
using Vaajak.Application.Services.Enrollment;
using Vaajak.Application.Services.Packages;
using Vaajak.Application.Services.Practice;
using Vaajak.Application.Services.Vocabs;

namespace Vaajak.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IVocabService, VocabService>();
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IPracticeService, PracticeService>();
            services.AddScoped<IArenaService, ArenaService>();
        }
    }
}
