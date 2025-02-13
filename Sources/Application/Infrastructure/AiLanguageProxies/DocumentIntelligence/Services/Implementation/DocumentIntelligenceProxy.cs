using Azure;
using Azure.AI.DocumentIntelligence;
using JetBrains.Annotations;
using Mmu.AiLanguageBuddy.Infrastructure.AiLanguageProxies.DocumentIntelligence.Models;
using Mmu.AiLanguageBuddy.Infrastructure.Settings.Provisioning.Services;

namespace Mmu.AiLanguageBuddy.Infrastructure.AiLanguageProxies.DocumentIntelligence.Services.Implementation
{
    [UsedImplicitly]
    public class DocumentIntelligenceProxy(ISettingsProvider settingsProvider) : IDocumentIntelligenceProxy
    {
        public async Task<IReadOnlyCollection<AnalyzedDocumentResult>> TransformFilesAsync(
            IReadOnlyCollection<string> documentFilePaths)
        {
            var client = new DocumentIntelligenceClient(
                new Uri(settingsProvider.AppSettings.DocumentIntelligenceEndpoint),
                new AzureKeyCredential(settingsProvider.AppSettings.DocumentIntelligenceApiKey));

            var result = new List<AnalyzedDocumentResult>();

            foreach (var filePath in documentFilePaths)
            {
                await using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                var operation = await client.AnalyzeDocumentAsync(
                    WaitUntil.Completed,
                    "prebuilt-read",
                    await BinaryData.FromStreamAsync(stream));

                await operation.WaitForCompletionAsync();

                result.Add(new AnalyzedDocumentResult(
                    filePath,
                    operation.Value.Content));
            }

            return result;
        }
    }
}