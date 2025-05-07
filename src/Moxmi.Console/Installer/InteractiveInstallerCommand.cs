namespace PleOps.Moxmi.Console.Installer;

using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PleOps.Moxmi.ModInstaller;
using PleOps.Moxmi.Platforms.Ekona;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Extensions;

[Description("Install a modding project or specific mod with user inputs")]
internal class InteractiveInstallerCommand : AsyncCommand<InteractiveInstallerCommand.Settings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        AppLoggerFactory.MinimumLevel = settings.Verbosity;

        var workflowProvider = new ModInstallerWorkflowProvider();
        workflowProvider.RegisterEkona();

        AnsiConsole.Write(new Rule("Mod installer"));
        var deserializer = new ModInstallerExtensibleSerializer();
        using var mixData = File.OpenRead(settings.ModPath);
        var mix = deserializer.Deserialize(mixData);
        AnsiConsole.MarkupLineInterpolated($"[bold blue]{mix.Mod.Name} v{mix.Mod.Version}[/]");
        AnsiConsole.MarkupLineInterpolated($"[italic gray]by {mix.Mod.Authors}[/]");
        AnsiConsole.MarkupLineInterpolated($"{mix.Mod.Description}");

        AnsiConsole.Write(new Rule("Compatibility verification"));
        CompatibleProductInfo? product = null;
        foreach (var compatibleProduct in mix.Mod.Compatibility) {
            AnsiConsole.MarkupLineInterpolated($"Checking match with {compatibleProduct.Name}");

            var verificationInfo = compatibleProduct.Verification;
            try {
                var validator = workflowProvider.GetCompatibilityValidator(verificationInfo.Method);

                var isCompatible = await validator.VerifyCompatibilityAsync(settings.SoftwarePath, verificationInfo.Hash);
                if (isCompatible) {
                    AnsiConsole.MarkupLineInterpolated($"[green]Match![/] Product ID: {compatibleProduct.ProductId}");
                    product = compatibleProduct;
                    break;
                }
            }
            catch (Exception) {
                AnsiConsole.MarkupLineInterpolated($"[red]Cannot validate with method {verificationInfo.Method}[/]");
            }
        }

        if (product is null) {
            AnsiConsole.MarkupLine("[red]No compatible product found[/]");
            return 1;
        }

        AnsiConsole.Write(new Rule("Software analysis"));
        // TODO: open it, then pass it for integrity check?

        AnsiConsole.Write(new Rule("Integrity verification"));
        var integrity = workflowProvider.GetIntegrityValidator(product.Format);
        var status = await integrity.VerifyIntegrityAsync(settings.SoftwarePath);
        AnsiConsole.MarkupLine($"Is data valid: {status.IsDataValid}");
        AnsiConsole.MarkupLine($"Signed: {status.HasValidPublisherSignature}");

        AnsiConsole.Write(new Rule("Installation"));
        AnsiConsole.MarkupLine("[gray]Applying resources...[/]");

        AnsiConsole.Write(new Rule("Bundle"));
        AnsiConsole.MarkupLine("[gray]Creating new software bundle...[/]");

        return 0;
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
            if (!File.Exists(SoftwarePath) && !Directory.Exists(SoftwarePath)) {
                return ValidationResult.Error($"The software path '{SoftwarePath}' does NOT exists");
            }

            if (!File.Exists(ModPath)) {
                return ValidationResult.Error($"The input mod file '{ModPath}' does NOT exists");
            }

            return base.Validate();
        }
    }
}
