using Lamar;
using Mmu.AiLanguageBuddy.Infrastructure.Options.Models;
using Mmu.AiLanguageBuddy.Infrastructure.Options.Services;
using Mmu.AiLanguageBuddy.Infrastructure.Options.Services.Implementation;

namespace Mmu.AiLanguageBuddy.Infrastructure.DependencyInjection
{
    internal static class ContainerFactory
    {
        internal static IContainer Create(PackageOptions options)
        {
            return new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    scanner.AssembliesFromApplicationBaseDirectory();
                    scanner.LookForRegistries();
                });

                cfg.For<IOptionsProvider>().Use(new OptionsProvider(options)).Singleton();
            });
        }
    }
}