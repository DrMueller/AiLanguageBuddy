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

        private static List<FoundLabel> RemoveOverlappingEntities(List<FoundLabel> labels)
        {
            labels.Sort((a, b) => a.Offset.CompareTo(b.Offset));

            var filteredList = new List<FoundLabel>();
            var lastEndOffset = -1;

            foreach (var label in labels)
            {
                var entityEnd = label.Offset + label.Length;

                if (label.Offset >= lastEndOffset)
                {
                    filteredList.Add(label);
                    lastEndOffset = entityEnd;
                }
            }

            return filteredList;
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
                            match.Index,
                            match.Length));
                    }
                }

                foundLabels = RemoveOverlappingEntities(foundLabels);
                entities.Add(new FoundEntity(configEntry.EntityName, foundLabels));
            }

            return new FoundDocumentEntities(
                filePath,
                text.Length,
                entities);
        }
    }
}