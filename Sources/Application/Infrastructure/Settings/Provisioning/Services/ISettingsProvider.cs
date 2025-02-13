using Mmu.AiLanguageBuddy.Infrastructure.Settings.Provisioning.Models;

namespace Mmu.AiLanguageBuddy.Infrastructure.Settings.Provisioning.Services
{
    public interface ISettingsProvider
    {
        AppSettings AppSettings { get; }
    }
}