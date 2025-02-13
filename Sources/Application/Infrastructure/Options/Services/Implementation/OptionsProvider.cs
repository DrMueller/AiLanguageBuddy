using Mmu.AiLanguageBuddy.Infrastructure.Options.Models;

namespace Mmu.AiLanguageBuddy.Infrastructure.Options.Services.Implementation
{
    internal class OptionsProvider(PackageOptions options) : IOptionsProvider
    {
        public PackageOptions Options { get; } = options;
    }
}