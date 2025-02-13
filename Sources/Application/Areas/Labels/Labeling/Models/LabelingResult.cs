namespace Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Models
{
    public record LabelingResult(IReadOnlyCollection<FoundDocumentEntities> Documents);
}