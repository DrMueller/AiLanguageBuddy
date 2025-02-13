using Mmu.AiLanguageBuddy.Areas.Labels.EntityConfig.Models;
using Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Models;

namespace Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Services.Servants
{
    public interface ILabeler
    {
        Task<LabelingResult> LabelAsync(
            string inputPath,
            EntityConfiguration entityConfig);
    }
}