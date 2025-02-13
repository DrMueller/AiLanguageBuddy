using System.IO.Abstractions;
using Mmu.AiLanguageBuddy.Areas.FileToText.Services.Servants;
using Mmu.AiLanguageBuddy.Infrastructure.AiLanguageProxies.DocumentIntelligence.Services;
using Mmu.AiLanguageBuddy.Infrastructure.Logging.Services;

namespace Mmu.AiLanguageBuddy.Areas.FileToText.Services.Implementation
{
    public class FileToTextService(
        IFileSystem fileSystem,
        IDocumentIntelligenceProxy intelligenceProxy,
        ILoggingService loggingService,
        ITextCleaner textCleaner)
        : IFileToTextService
    {
        // See https://learn.microsoft.com/en-us/azure/ai-services/document-intelligence/prebuilt/read?view=doc-intel-4.0.0&tabs=sample-code
        private static readonly IReadOnlyCollection<string> _supportedExtensions = new List<string>
        {
            "jpeg",
            "jpg",
            "png",
            "bmp",
            "tiff",
            "heif",
            "docx",
            "xlsx",
            "xls",
            "pptx",
            "html",
            "pdf"
        };

        public async Task TranformAsync(string inputPath, string outputPath)
        {
            var filePaths = fileSystem.Directory.GetFiles(inputPath, "*.*", SearchOption.AllDirectories);
            loggingService.LogInfo($"Found {filePaths.Length} files..");
            var supportedFilePaths = filePaths.Where(f => _supportedExtensions.Contains(fileSystem.Path.GetExtension(f).TrimStart('.').ToLower())).ToList();
            loggingService.LogInfo($"Found {supportedFilePaths.Count} supported files..");

            if (!fileSystem.Directory.Exists(outputPath))
            {
                fileSystem.Directory.CreateDirectory(outputPath);
            }

            foreach (var file in supportedFilePaths)
            {
                try
                {
                    loggingService.LogInfo($"Analyzing {fileSystem.Path.GetFileName(file)}..");

                    var analysisResults = await intelligenceProxy.TransformFilesAsync([file]);

                    foreach (var analysisResult in analysisResults)
                    {
                        loggingService.LogInfo($"Saving {analysisResult.FilePath}..");
                        var cleanedText = textCleaner.CleanText(analysisResult.Content);
                        var fileName = fileSystem.Path.GetFileName(analysisResult.FilePath);
                        var targetName = fileSystem.Path.Combine(outputPath, fileName);
                        targetName = fileSystem.Path.ChangeExtension(targetName, ".txt");
                        await fileSystem.File.WriteAllTextAsync(targetName, cleanedText);

                        fileSystem.File.Delete(file);
                    }
                }
                catch (Exception)
                {
                    var failureDir = fileSystem.Path.Combine(outputPath, "Failures");

                    if (!fileSystem.Directory.Exists(failureDir))
                    {
                        fileSystem.Directory.CreateDirectory(failureDir);
                    }

                    var fileName = fileSystem.Path.GetFileName(file);
                    var targetName = fileSystem.Path.Combine(failureDir, fileName);

                    if (fileSystem.File.Exists(targetName))
                    {
                        var directory = fileSystem.Path.GetDirectoryName(targetName);
                        var extension = fileSystem.Path.GetExtension(targetName);
                        var fileNameWithoutExt = fileSystem.Path.GetFileNameWithoutExtension(targetName);
                        targetName = fileSystem.Path.Combine(directory!, $"{fileNameWithoutExt}_{Guid.NewGuid().ToString()}{extension}");
                    }

                    fileSystem.File.Move(file, targetName);
                }
            }

            loggingService.LogInfo("Transformation done.");
        }
    }
}