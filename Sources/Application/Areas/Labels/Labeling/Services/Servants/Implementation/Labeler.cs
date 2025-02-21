using System.IO.Abstractions;
using System.Text.RegularExpressions;
using Mmu.AiLanguageBuddy.Areas.Labels.EntityConfig.Models;
using Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Models;

namespace Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Services.Servants.Implementation
{
    public class Labeler(IFileSystem fileSystem) : ILabeler
    {
        public Task<LabelingResult> LabelAsync(
            string inputPath,
            EntityConfiguration entityConfig)
        {
            var textFiles = fileSystem.Directory.GetFiles(inputPath, "*.txt");

            var docEntities = textFiles
                .Select(filePath => FindEntities(filePath, entityConfig))
                .ToList();

            var labelingResult = new LabelingResult(docEntities);

            return Task.FromResult(labelingResult);
        }

        private FoundDocumentEntities FindEntities(string filePath, EntityConfiguration config)
        {
            var entities = new List<FoundEntity>();
            var text = fileSystem.File.ReadAllText(filePath);

            foreach (var configEntry in config.Entries)
            {
                var foundLabels = new List<FoundLabel>();

                foreach (var label in configEntry.Labels)
                {
                    var pattern = $@"\b{Regex.Escape(label)}\b";
                    var matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

                    foreach (Match match in matches)
                    {
                        foundLabels.Add(new FoundLabel(
                            configEntry.EntityName,
                            match.ToString(),
                            match.Index,
                            match.Length));
                    }
                }

                entities.Add(new FoundEntity(configEntry.EntityName, foundLabels));
            }

            var allLabels = entities
                .SelectMany(f => f.Labels)
                .ToList();

            entities.ForEach(e => e.RemoveDuplicates(allLabels));

            return new FoundDocumentEntities(
                filePath,
                text.Length,
                entities);
        }
    }
}