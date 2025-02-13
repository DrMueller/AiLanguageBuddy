using Mmu.AiLanguageBuddy.Infrastructure.AiLanguageProxies.DocumentIntelligence.Models;

namespace Mmu.AiLanguageBuddy.Infrastructure.AiLanguageProxies.DocumentIntelligence.Services
{
    public interface IDocumentIntelligenceProxy
    {
        Task<IReadOnlyCollection<AnalyzedDocumentResult>> TransformFilesAsync(
            IReadOnlyCollection<string> documentFilePaths);
    }
}