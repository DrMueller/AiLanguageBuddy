using Mmu.AiLanguageBuddy.Infrastructure.Logging.Models;
using Mmu.AiLanguageBuddy.Infrastructure.Options.Models;

namespace Mmu.AiLanguageBuddy.Infrastructure.Logging.Services.Servants.Implementation
{
    public class ConsoleOutputWriter : IOutputWriter
    {
        private static readonly IDictionary<LogLevel, (ConsoleColor?, ConsoleColor?)> _colorMap = new Dictionary<LogLevel, (ConsoleColor?, ConsoleColor?)>
        {
            { LogLevel.Debug, (null, null) },
            { LogLevel.Info, (null, null) },
            { LogLevel.Warning, (null, ConsoleColor.DarkYellow) },
            { LogLevel.Error, (null, ConsoleColor.Red) }
        };

        public SystemMode SystemMode => SystemMode.Console;

        public void Write(LogLevel loglevel, string message)
        {
            var colors = _colorMap[loglevel];
            AdjustColors(colors.Item1, colors.Item2);
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void AdjustColors(ConsoleColor? backgroundColor, ConsoleColor? foregroundColor)
        {
            if (backgroundColor.HasValue)
            {
                Console.BackgroundColor = backgroundColor.Value;
            }

            if (foregroundColor.HasValue)
            {
                Console.ForegroundColor = foregroundColor.Value;
            }
        }
    }
}