using Mmu.AiLanguageBuddy.Infrastructure.Logging.Models;
using Mmu.AiLanguageBuddy.Infrastructure.Options.Models;

namespace Mmu.AiLanguageBuddy.Infrastructure.Logging.Services.Servants
{
    public interface IOutputWriter
    {
        public SystemMode SystemMode { get; }
        void Write(LogLevel loglevel, string message);
    }
}