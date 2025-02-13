using JetBrains.Annotations;

namespace Mmu.AiLanguageBuddy.Infrastructure.Settings.Provisioning.Models
{
    [PublicAPI]
    public class AppSettings
    {
        public const string SectionKey = "AppSettings";
        public string DocumentIntelligenceApiKey { get; set; } = default!;
        public string DocumentIntelligenceEndpoint { get; set; } = default!;
    }
}