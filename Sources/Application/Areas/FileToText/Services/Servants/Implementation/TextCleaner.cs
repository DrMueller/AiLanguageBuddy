namespace Mmu.AiLanguageBuddy.Areas.FileToText.Services.Servants.Implementation
{
    public class TextCleaner : ITextCleaner
    {
        public string CleanText(string text)
        {
            return text.Replace("- ", string.Empty);
        }
    }
}