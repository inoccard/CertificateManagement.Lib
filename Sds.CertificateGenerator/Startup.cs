using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;
using Sds.CertificateGenerator.Contracts;
using Sds.CertificateGenerator.Services;

namespace Sds.CertificateGenerator
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Startup
    {
        /// <summary>
        /// Configure Injection Dependency Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="licenseType"></param>
        public static void AddCertificateStartup(this IServiceCollection services,
            LicenseType licenseType = LicenseType.Community)
        {
            services.AddScoped<ICertificateGenerator>(_ =>
                new CertificateGeneratorService(licenseType));
        }
    }
}