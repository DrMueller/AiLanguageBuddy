using Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Models;
using Mmu.AiLanguageBuddy.Infrastructure.AiLanguageProxies.CustomNer.Models;

namespace Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Services.Servants
{
    public interface IProjectDataMapper
    {
        ProjectData Map(LabelingResult labelingResult);
    }
}