using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;
using Sds.CertificateManagement.Contracts;
using Sds.CertificateManagement.Services;

namespace Sds.CertificateManagement
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