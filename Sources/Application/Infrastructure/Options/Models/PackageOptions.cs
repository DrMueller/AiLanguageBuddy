using CommandLine;
using JetBrains.Annotations;

namespace Mmu.AiLanguageBuddy.Infrastructure.Options.Models
{
    [PublicAPI]
    public class PackageOptions
    {
        [Option('c', "command")]
        public string Command { get; set; } = default!;

        [Option('n', "config-path")]
        public string ConfigPath { get; set; } = default!;

        [Option('i', "input-path")]
        public string InputPath { get; set; } = null!;

        [Option('o', "output-path")]
        public string OutputPath { get; set; } = null!;

        [Option('s', "system-mode", Default = SystemMode.Console)]
        public SystemMode SystemMode { get; set; }
    }
}