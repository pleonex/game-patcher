namespace PleOps.Moxmi.Console.Installer;

using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;

[Description("Install a modding project or specific mod with user inputs")]
internal class InteractiveInstallerCommand : AsyncCommand<InteractiveInstallerCommand.Settings>
{
    private ILogger<InteractiveInstallerCommand> logger = null!;

    public override Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        AppLoggerFactory.MinimumLevel = settings.Verbosity;
        logger = AppLoggerFactory.CreateLogger<InteractiveInstallerCommand>();

        logger.LogInformation("Starting to apply mod!");

        return Task.FromResult(0);
    }

    public sealed class Settings : CommandSettings
    {
        [CommandArgument(0, "<SOFTWARE_PATH>")]
        [Description("Path to the software to apply the mod")]
        public required string SoftwarePath { get; set; }


        [CommandArgument(1, "<MOD_PATH>")]
        [Description("Path to the .mdp or .mix file")]
        public required string ModPath { get; set; }

        [CommandArgument(2, "<OUTPUT_PATH>")]
        [Description("Path to place the modded software")]
        public required string OutputPath { get; set; }

        [CommandOption("-v|--verbosity")]
        [Description("Logging output verbosity")]
        [DefaultValue(LogLevel.Warning)]
        public LogLevel Verbosity { get; set; }

        public override ValidationResult Validate()
        {
            if (!File.Exists(SoftwarePath)) {
                return ValidationResult.Error($"The input software file '{SoftwarePath}' does NOT exists");
            }

            if (!File.Exists(ModPath)) {
                return ValidationResult.Error($"The input mod file '{ModPath}' does NOT exists");
            }

            return base.Validate();
        }
    }
}
