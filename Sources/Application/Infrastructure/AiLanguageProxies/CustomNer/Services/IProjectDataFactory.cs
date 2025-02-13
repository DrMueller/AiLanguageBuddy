using Mmu.AiLanguageBuddy.Infrastructure.AiLanguageProxies.CustomNer.Models;

namespace Mmu.AiLanguageBuddy.Infrastructure.AiLanguageProxies.CustomNer.Services
{
    public interface IProjectDataFactory
    {
        ProjectData CreateSkeleton();
    }
}