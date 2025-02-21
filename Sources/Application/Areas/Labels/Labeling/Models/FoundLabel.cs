namespace Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Models
{
    public class FoundLabel(
        string categoryName,
        string labelText,
        int offset,
        int length)
    {
        public string CategoryName { get; } = categoryName;
        public string LabelText { get; } = labelText;
        public int Length { get; } = length;
        public int Offset { get; } = offset;

        public bool OverlapsWithAny(IReadOnlyCollection<FoundLabel> other)
        {
            foreach (var otherLabel in other)
            {
                if (OverlapsWith(otherLabel))
                {
                    return true;
                }
            }

            return false;
        }

        private bool OverlapsWith(FoundLabel other)
        {
            if (other == this)
            {
                return false;
            }

            var thisEnd = Offset + Length;
            var otherEnd = other.Offset + other.Length;

            return Offset <= otherEnd && thisEnd >= other.Offset;
        }
    }
}