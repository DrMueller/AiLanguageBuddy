using JetBrains.Annotations;
using Mmu.AiLanguageBuddy.Infrastructure.Logging.Models;
using Mmu.AiLanguageBuddy.Infrastructure.Logging.Services.Servants;
using Mmu.AiLanguageBuddy.Infrastructure.Options.Services;

namespace Mmu.AiLanguageBuddy.Infrastructure.Logging.Services.Implementation
{
    [UsedImplicitly]
    public class LoggingService(
        IOptionsProvider optionsProvider,
        IEnumerable<IOutputWriter> outputWriters)
        : ILoggingService
    {
        public void LogDebug(string message)
        {
            GetOutputWriter().Write(LogLevel.Debug, message);
        }

        public void LogError(string message)
        {
            GetOutputWriter().Write(LogLevel.Error, message);
        }

        public void LogException(Exception ex)
        {
            var message = $"{ex.Message};{ex.StackTrace}";
            GetOutputWriter().Write(LogLevel.Error, message);
        }

        public void LogInfo(string message)
        {
            GetOutputWriter().Write(LogLevel.Info, message);
        }

        private IOutputWriter GetOutputWriter()
        {
            return
                outputWriters
                    .Single(f => f.SystemMode == optionsProvider.Options.SystemMode);
        }
    }
}