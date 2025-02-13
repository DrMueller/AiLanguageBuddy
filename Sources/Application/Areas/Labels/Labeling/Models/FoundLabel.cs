namespace Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Models
{
    public class FoundLabel(
        string labelName,
        int offset,
        int length)
    {
        public string LabelName { get; } = labelName;
        public int Length { get; } = length;
        public int Offset { get; } = offset;
    }
}