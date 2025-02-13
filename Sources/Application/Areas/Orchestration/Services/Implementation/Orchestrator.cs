using Mmu.AiLanguageBuddy.Areas.FileToText.Services;
using Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Services;
using Mmu.AiLanguageBuddy.Areas.Orchestration.Models;
using Mmu.AiLanguageBuddy.Infrastructure.Options.Services;

namespace Mmu.AiLanguageBuddy.Areas.Orchestration.Services.Implementation
{
    public class Orchestrator(
        IOptionsProvider optionsProvider,
        IFileToTextService fileToTextService,
        ILabelingService labelingService)
        : IOrchestrator
    {
        public async Task OrchestrateAsync()
        {
            if (optionsProvider.Options.Command == Commands.FileToText)
            {
                await fileToTextService.TranformAsync(optionsProvider.Options.InputPath, optionsProvider.Options.OutputPath);
            }

            if (optionsProvider.Options.Command == Commands.Label)
            {
                await labelingService.LabelAsync();
            }
        }
    }
}