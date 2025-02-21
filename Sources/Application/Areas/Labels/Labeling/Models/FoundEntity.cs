namespace Mmu.AiLanguageBuddy.Areas.Labels.Labeling.Models
{
    public class FoundEntity
    {
        private readonly IList<FoundLabel> _labels;

        public string Entity { get; }
        public IReadOnlyCollection<FoundLabel> Labels => _labels.ToList();

        public FoundEntity(string entity,
            IReadOnlyCollection<FoundLabel> labels)
        {
            Entity = entity;
            _labels = labels.ToList();
        }

        public void RemoveDuplicates(IReadOnlyCollection<FoundLabel> allLabels)
        {
            var labelsToRemove = new List<FoundLabel>();

            foreach (var label in _labels)
            {
                if (label.OverlapsWithAny(allLabels))
                {
                    labelsToRemove.Add(label);
                }
            }

            labelsToRemove.ForEach(label => _labels.Remove(label));
        }
    }
}