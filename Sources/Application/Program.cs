using JetBrains.Annotations;
using Mmu.AiLanguageBuddy.Areas.Orchestration.Services;
using Mmu.AiLanguageBuddy.Infrastructure.DependencyInjection;
using Mmu.AiLanguageBuddy.Infrastructure.Options.Services.Implementation;

namespace Mmu.AiLanguageBuddy
{
    [PublicAPI]
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var options = OptionsFactory.TryCreating(args);

            if (options == null)
            {
                return;
            }

            var container = ContainerFactory.Create(options);
            var orchestrator = container.GetInstance<IOrchestrator>();
            await orchestrator.OrchestrateAsync();
        }
    }
}