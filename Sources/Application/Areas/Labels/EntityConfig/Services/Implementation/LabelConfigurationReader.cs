using System.IO.Abstractions;
using Mmu.AiLanguageBuddy.Areas.Labels.EntityConfig.Models;

namespace Mmu.AiLanguageBuddy.Areas.Labels.EntityConfig.Services.Implementation
{
    public class LabelConfigurationReader(IFileSystem fileSystem) : ILabelConfigurationReader
    {
        public Task<EntityConfiguration> ReadAsync(string configFilePath)
        {
            var configLines = fileSystem.File.ReadAllLines(configFilePath);
            var configDict = new Dictionary<string, List<string>>();

            foreach (var line in configLines)
            {
                if (line.Trim() == string.Empty)
                {
                    continue;
                }

                var entityName = line.Split(';').ElementAt(0);

                if (configDict.ContainsKey(entityName))
                {
                    configDict[entityName].AddRange(line.Split(';').Skip(1));
                }
                else
                {
                    configDict[entityName] = line.Split(';').Skip(1).ToList();
                }
            }

            var configEntries = configDict
                .Select(kvp => new EntityConfigurationEntry(
                    kvp.Key,
                    kvp.Value
                        .Where(f => !string.IsNullOrEmpty(f))
                        .Where(f => f.Length >= 3)
                        .Distinct()
                        .OrderByDescending(f => f.Length)
                        .ToList()))
                .ToList();

            return Task.FromResult(new EntityConfiguration(configEntries));
        }
    }
}