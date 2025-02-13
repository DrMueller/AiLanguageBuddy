using System.Text.Json.Serialization;
using JetBrains.Annotations;

// See https://learn.microsoft.com/en-us/azure/ai-services/language-service/custom-named-entity-recognition/concepts/data-formats
namespace Mmu.AiLanguageBuddy.Infrastructure.AiLanguageProxies.CustomNer.Models
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    [PublicAPI]
    public class ProjectData
    {
        [JsonPropertyName("assets")]
        public Assets Assets { get; set; }

        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }

        [JsonPropertyName("projectFileVersion")]
        public string ProjectFileVersion { get; set; }

        [JsonPropertyName("stringIndexType")]
        public string StringIndexType { get; set; }
    }

    [PublicAPI]
    public class Metadata
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("multilingual")]
        public bool Multilingual { get; set; }

        [JsonPropertyName("projectKind")]
        public string ProjectKind { get; set; }

        [JsonPropertyName("projectName")]
        public string ProjectName { get; set; }

        [JsonPropertyName("settings")]
        public Dictionary<string, object> Settings { get; set; }

        [JsonPropertyName("storageInputContainerName")]
        public string StorageInputContainerName { get; set; }
    }

    [PublicAPI]
    public class Assets
    {
        [JsonPropertyName("documents")]
        public List<Document> Documents { get; set; }

        [JsonPropertyName("entities")]
        public List<Entity> Entities { get; set; }

        [JsonPropertyName("projectKind")]
        public string ProjectKind { get; set; }
    }

    [PublicAPI]
    public class Entity
    {
        [JsonPropertyName("category")]
        public string Category { get; set; }
    }

    [PublicAPI]
    public class Document
    {
        [JsonPropertyName("dataset")]
        public string Dataset { get; set; }

        [JsonPropertyName("entities")]
        public List<DocumentEntity> Entities { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }
    }

    [PublicAPI]
    public class DocumentEntity
    {
        [JsonPropertyName("labels")]
        public List<Label> Labels { get; set; }

        //[JsonPropertyName("regionLength")]
        //public int RegionLength { get; set; }

        //[JsonPropertyName("regionOffset")]
        //public int RegionOffset { get; set; }
    }

    [PublicAPI]
    public class Label
    {
        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("length")]
        public int Length { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.