namespace Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Models
{
    public record FoundEntity(
        string Entity,
        IReadOnlyCollection<FoundLabel> Labels);
}