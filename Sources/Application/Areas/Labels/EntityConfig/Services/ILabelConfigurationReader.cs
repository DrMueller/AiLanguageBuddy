using Mmu.AiLanguageBuddy.Areas.Labels.EntityConfig.Models;

namespace Mmu.AiLanguageBuddy.Areas.Labels.EntityConfig.Services
{
    public interface ILabelConfigurationReader
    {
        Task<EntityConfiguration> ReadAsync(string configFilePath);
    }
}