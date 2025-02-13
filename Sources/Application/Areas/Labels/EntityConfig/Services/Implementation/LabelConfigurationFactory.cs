using System.IO.Abstractions;
using System.Text;

namespace Mmu.AiLanguageBuddy.Areas.Labels.EntityConfig.Services.Implementation
{
    public class LabelConfigurationFactory(IFileSystem fileSystem) : ILabelConfigurationFactory
    {
        public async Task CreateAsync()
        {
            var skills = fileSystem.File
                .ReadAllLines(@"C:\MatthiasStuff\AILanguageBuddy\Skills.txt")
                .Select(f => f.Trim())
                .Distinct()
                .ToList();

            skills.RemoveAt(0);

            var skillsList = string.Join(";", skills);

            var roles = fileSystem.File
                .ReadAllLines(@"C:\MatthiasStuff\AILanguageBuddy\Roles.txt")
                .Select(f => f.Trim())
                .Distinct()
                .ToList();

            roles.RemoveAt(0);
            var rolesList = string.Join(";", roles);

            var sb = new StringBuilder();
            sb.AppendLine($"Skill;{skillsList}");
            sb.Append($"Role;{rolesList}");

            await fileSystem.File.WriteAllTextAsync(
                @"C:\MatthiasStuff\AILanguageBuddy\Config.txt",
                sb.ToString());
        }
    }
}