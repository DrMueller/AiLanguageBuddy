namespace Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Models
{
    public class FoundDocumentEntities(
        string filePath,
        int documentTextLength,
        IReadOnlyCollection<FoundEntity> foundEntities)
    {
        public int DocumentTextLength { get; } = documentTextLength;
        public string FilePath { get; } = filePath;
        public IReadOnlyCollection<FoundEntity> FoundEntities { get; } = foundEntities;
    }
}