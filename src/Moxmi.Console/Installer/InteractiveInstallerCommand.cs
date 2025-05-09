namespace PleOps.Moxmi.Console.Installer;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PleOps.Moxmi.ModInstaller;
using PleOps.Moxmi.Platforms.Ekona;
using Spectre.Console;
using Spectre.Console.Cli;
using Yarhl.FileSystem;

[Description("Install a modding project or specific mod with user inputs")]
internal class InteractiveInstallerCommand : AsyncCommand<InteractiveInstallerCommand.Settings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        AppLoggerFactory.MinimumLevel = settings.Verbosity;

        int result = await AnsiConsole.Status().StartAsync(
            "Initializing mod installer",
            async ctx => {
                var workflowProvider = new ModInstallerWorkflowProvider()
                    .RegisterEkona();

                ctx.Status = "Reading the mod installer";
                ModInstallerExtensibleManifest mix = ReadMix(settings.ModPath);

                ctx.Status = "Checking the compatibility of the mod";
                AnsiConsole.WriteLine();
                var product = await GetCompatibleProductAsync(mix, settings.SoftwarePath, workflowProvider);
                if (product is null) {
                    return 1;
                }

                ctx.Status = "Checking software integrity";
                AnsiConsole.WriteLine();
                bool isValid = await VerifyIntegrityAsync(settings.SoftwarePath, product, workflowProvider);
                if (!isValid) {
                    return 2;
                }

                // TODO: ask for features
                // TODO: ask for parameters

                ctx.Status = "Opening software";
                AnsiConsole.WriteLine();
                using Node? software = await ReadSoftwareAsync(settings.SoftwarePath, product, workflowProvider);
                if (software is null) {
                    return 3;
                }

                ctx.Status = "Applying mod resources";
                AnsiConsole.WriteLine();
                bool installSuccess = await ApplyModResourcesAsync(software, mix.Resources, workflowProvider);
                if (!installSuccess) {
                    return 4;
                }

                ctx.Status = "Creating output bundle";
                AnsiConsole.WriteLine();
                await Task.Delay(2_000);
                AnsiConsole.MarkupLine("Creating bundle... [green]done[/]");

                return 0;
            });
        return result;

    }

    private static ModInstallerExtensibleManifest ReadMix(string modPath)
    {
        var deserializer = new ModInstallerExtensibleSerializer();
        using var mixData = File.OpenRead(modPath);
        ModInstallerExtensibleManifest mix = deserializer.Deserialize(mixData);
        AnsiConsole.MarkupLine("Reading MIX... [green]done[/]");

        var panel = new Panel($"[italic gray]by {mix.Mod.Authors}[/]\n{mix.Mod.Description.EscapeMarkup()}");
        panel.Header($"[bold blue]{mix.Mod.Name} v{mix.Mod.Version}[/]");
        AnsiConsole.Write(panel);

        return mix;
    }

    private static async Task<CompatibleProductInfo?> GetCompatibleProductAsync(
        ModInstallerExtensibleManifest mix,
        string softwarePath,
        ModInstallerWorkflowProvider workflowProvider)
    {
        CompatibleProductInfo? product = null;
        foreach (var compatibleProduct in mix.Mod.Compatibility) {
            AnsiConsole.MarkupLineInterpolated($"Checking match with {compatibleProduct.Name}");

            bool matchAllValidators = true;
            foreach (var verificationInfo in compatibleProduct.Verification) {
                var validator = workflowProvider.GetCompatibilityValidator(verificationInfo.Method);
                if (validator is null) {
                    AnsiConsole.MarkupLineInterpolated($"[red]Cannot validate with method {verificationInfo.Method}[/]");
                    matchAllValidators = false;
                    break;
                }

                var isCompatible = await validator.VerifyCompatibilityAsync(softwarePath, verificationInfo.Value);
                matchAllValidators = matchAllValidators && isCompatible;
                if (!isCompatible) {
                    break;
                }
            }

            if (matchAllValidators) {
                AnsiConsole.MarkupLineInterpolated($"Checking compatibility... [green]done[/] -> {compatibleProduct.ProductId}");
                product = compatibleProduct;
                break;
            }
        }

        if (product is null) {
            AnsiConsole.MarkupLine("[red]No compatible product found[/]");
            return null;
        }

        return product;
    }

    private static async Task<bool> VerifyIntegrityAsync(
        string softwarePath,
        CompatibleProductInfo product,
        ModInstallerWorkflowProvider workflowProvider)
    {
        var integrity = workflowProvider.GetIntegrityValidator(product.Format);
        if (integrity is null) {
            AnsiConsole.MarkupLineInterpolated($"[red]Cannot find integrity validator for {product.Format}[/]");
            return false;
        }

        var status = await integrity.VerifyIntegrityAsync(softwarePath);

        AnsiConsole.MarkupLine("Checking integrity... [green]done[/]");
        AnsiConsole.MarkupLine($"Is data valid: {status.IsDataValid}");
        AnsiConsole.MarkupLine($"Signed: {status.HasValidPublisherSignature}");

        return status.IsDataValid != Integrity.IntegrityVerificationResult.Invalid;
    }

    private static async Task<Node?> ReadSoftwareAsync(
        string softwarePath,
        CompatibleProductInfo product,
        ModInstallerWorkflowProvider provider)
    {
        var reader = provider.GetSoftwareReader(product.Format);
        if (reader is null) {
            return null;
        }

        Node node = await reader.OpenPathAsync(softwarePath);
        AnsiConsole.MarkupLine("Software reading... [green]done![/]");

        return node;
    }

    private static async Task<bool> ApplyModResourcesAsync(
        Node software,
        Collection<Resource> resources,
        ModInstallerWorkflowProvider provider)
    {
        // TODO: filter resources for selected features
        foreach (var resource in resources) {
            var installer = provider.GetResourceInstaller(resource.InstallationMethod);
            if (installer is null) {
                AnsiConsole.MarkupLineInterpolated($"[red]Cannot find installer for method: {resource.InstallationMethod}[/]");
                return false;
            }

            // TODO: get resource
            // TODO: get options
            AnsiConsole.MarkupLineInterpolated($"Applying resources: [gray]{resource.Name}[/]");
            await installer.InstallResourceAsync(software, null, null);
        }

        AnsiConsole.MarkupLine("Mod resources... [green]applied[/]");
        return true;
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
