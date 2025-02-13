namespace Mmu.AiLanguageBuddy.Areas.FileToText.Services
{
    public interface IFileToTextService
    {
        Task TranformAsync(string inputPath, string outputPath);
    }
}