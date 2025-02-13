using Mmu.AiLanguageBuddy.Infrastructure.Logging.Models;
using Mmu.AiLanguageBuddy.Infrastructure.Options.Models;

namespace Mmu.AiLanguageBuddy.Infrastructure.Logging.Services.Servants.Implementation
{
    public class AzureDevOpsOutputWriter : IOutputWriter
    {
        private static readonly IDictionary<LogLevel, string> _prefixMap = new Dictionary<LogLevel, string>
        {
            { LogLevel.Debug, "##vso[task.debug]" },
            { LogLevel.Info, string.Empty },
            { LogLevel.Warning, "##vso[task.issue type=warning;]" },
            { LogLevel.Error, "##vso[task.issue type=error;]" }
        };

        public SystemMode SystemMode => SystemMode.AzureDevOps;

        public void Write(LogLevel loglevel, string message)
        {
            var azureDevOpsPrefix = _prefixMap[loglevel];
            Console.WriteLine($"{azureDevOpsPrefix}{message}");
        }
    }
}