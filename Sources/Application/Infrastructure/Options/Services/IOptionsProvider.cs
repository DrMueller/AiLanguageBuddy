using Mmu.AiLanguageBuddy.Infrastructure.Options.Models;

namespace Mmu.AiLanguageBuddy.Infrastructure.Options.Services
{
    public interface IOptionsProvider
    {
        PackageOptions Options { get; }
    }
}