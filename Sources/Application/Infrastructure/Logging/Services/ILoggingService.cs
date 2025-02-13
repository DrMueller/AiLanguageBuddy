namespace Mmu.AiLanguageBuddy.Infrastructure.Logging.Services
{
    public interface ILoggingService
    {
        void LogDebug(string message);
        void LogError(string message);
        void LogException(Exception ex);
        void LogInfo(string message);
    }
}