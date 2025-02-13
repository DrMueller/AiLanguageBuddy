using CommandLine;
using JetBrains.Annotations;
using Mmu.AiLanguageBuddy.Areas.Orchestration.Models;
using Mmu.AiLanguageBuddy.Infrastructure.Options.Models;

namespace Mmu.AiLanguageBuddy.Infrastructure.Options.Services.Implementation
{
    [UsedImplicitly]
    public static class OptionsFactory
    {
        public static PackageOptions? TryCreating(string[] args)
        {
            var result = Parser.Default.ParseArguments<PackageOptions>(args);

            if (result.Errors.Any())
            {
                WriteError(string.Join(Environment.NewLine, result.Errors.Select(f => f.ToString())));
            }

            return ValidateOptions(result.Value);
        }

        private static PackageOptions? ValidateOptions(PackageOptions options)
        {
            if (options.Command == Commands.FileToText)
            {
                if (string.IsNullOrEmpty(options.InputPath) || string.IsNullOrEmpty(options.OutputPath))
                {
                    WriteError("Command FileToText requires input-path and output-path.");

                    return null;
                }
            }

            return options;
        }

        private static void WriteError(string error)
        {
            Console.WriteLine(error);
        }
    }
}