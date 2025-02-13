using System.IO.Abstractions;
using Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Models;
using Mmu.AiLanguageBuddy.Infrastructure.AiLanguageProxies.CustomNer.Models;
using Mmu.AiLanguageBuddy.Infrastructure.AiLanguageProxies.CustomNer.Services;

namespace Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Services.Servants.Implementation
{
    public class ProjectDataMapper(
        IProjectDataFactory projectDataFactory,
        IFileSystem fileSystem) : IProjectDataMapper
    {
        public ProjectData Map(LabelingResult labelingResult)
        {
            var projectData = projectDataFactory.CreateSkeleton();
            AppendEntities(projectData, labelingResult);

            foreach (var document in labelingResult.Documents)
            {
                var labels = document.FoundEntities
                    .SelectMany(f => f.Labels)
                    .Select(label => new Label
                    {
                        Category = label.LabelName,
                        Length = label.Length,
                        Offset = label.Offset
                    })
                    .ToList();

                if (labels.Any())
                {
                    projectData.Assets.Documents.Add(new Document
                    {
                        Dataset = "Train",
                        Location = fileSystem.Path.GetFileName(document.FilePath),
                        Language = "de",
                        Entities =
                        [
                            new DocumentEntity
                            {
                                Labels = labels
                            }
                        ]
                    });
                }
            }

            return projectData;
        }

        private static void AppendEntities(ProjectData projectData, LabelingResult labelingResult)
        {
            projectData.Assets.Entities = labelingResult
                .Documents
                .SelectMany(f => f.FoundEntities)
                .Select(f => f.Entity)
                .Distinct()
                .Select(f => new Entity
                {
                    Category = f
                }).ToList();
        }
    }
}