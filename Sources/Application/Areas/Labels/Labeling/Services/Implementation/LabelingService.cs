using System.IO.Abstractions;
using System.Text.Json;
using Mmu.AiLanguageBuddy.Areas.Labels.EntityConfig.Services;
using Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Services.Servants;
using Mmu.AiLanguageBuddy.Infrastructure.Options.Services;

namespace Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Services.Implementation
{
    public class LabelingService(
        IFileSystem fileSystem,
        ILabeler labeler,
        IOptionsProvider optionsProvider,
        ILabelConfigurationReader configReader,
        IProjectDataMapper projectDataMapper)
        : ILabelingService
    {
        public async Task LabelAsync()
        {
            var config = await configReader.ReadAsync(optionsProvider.Options.ConfigPath);
            var labelingResult = await labeler.LabelAsync(
                optionsProvider.Options.InputPath,
                config);

            var projectData = projectDataMapper.Map(labelingResult);

            var json = JsonSerializer.Serialize(projectData);

            await fileSystem
                .File.WriteAllTextAsync(
                    optionsProvider.Options.OutputPath,
                    json);
        }
    }
}