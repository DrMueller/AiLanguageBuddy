using System.IO.Abstractions;
using JetBrains.Annotations;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using Mmu.AiLanguageBuddy.Infrastructure.Logging.Services.Servants;
using Mmu.AiLanguageBuddy.Infrastructure.Settings.Config.Services;
using Mmu.AiLanguageBuddy.Infrastructure.Settings.Provisioning.Models;

namespace Mmu.AiLanguageBuddy.Infrastructure.DependencyInjection
{
    [UsedImplicitly]
    public class ServiceRegistryCollection : ServiceRegistry
    {
        public ServiceRegistryCollection()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<ServiceRegistryCollection>();
                scanner.AddAllTypesOf<IOutputWriter>(ServiceLifetime.Singleton);
                scanner.WithDefaultConventions();
            });

            var config = ConfigurationFactory.Create();
            this.Configure<AppSettings>(config.GetSection(AppSettings.SectionKey));

            For<IFileSystem>().Use<FileSystem>().Scoped();
        }
    }
}