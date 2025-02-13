using Mmu.AiLanguageBuddy.Infrastructure.AiLanguageProxies.CustomNer.Models;

namespace Mmu.AiLanguageBuddy.Infrastructure.AiLanguageProxies.CustomNer.Services.Implementation
{
    public class ProjectDataFactory : IProjectDataFactory
    {
        public ProjectData CreateSkeleton()
        {
            var project = new ProjectData
            {
                Assets = new Assets
                {
                    ProjectKind = "CustomEntityRecognition",
                    Documents = new List<Document>(),
                    Entities = new List<Entity>()
                },
                Metadata = new Metadata
                {
                    ProjectKind = "CustomEntityRecognition",
                    Description = "Project1",
                    Language = "de",
                    Multilingual = true,
                    ProjectName = "Project1",
                    Settings = new Dictionary<string, object>(),
                    StorageInputContainerName = "blob"
                },
                ProjectFileVersion = "2022-05-01",
                StringIndexType = "Utf16CodeUnit"
            };

            return project;
        }
    }
}