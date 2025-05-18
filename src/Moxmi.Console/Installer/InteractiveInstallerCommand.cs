namespace PleOps.Moxmi.Console.Installer;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PleOps.Moxmi;
using PleOps.Moxmi.ModInstallerExtensible;
using PleOps.Moxmi.ModResources;
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

        var serviceProvider = new ModInstallerServiceProvider()
            .RegisterEkona();

        using MixPackage mix = ReadMix(settings.ModPath);
        MixManifest manifest = mix.Manifest;

        AnsiConsole.WriteLine();
        var product = await GetCompatibleProductAsync(manifest, settings.SoftwarePath, serviceProvider);
        if (product is null) {
            return 1;
        }

        AnsiConsole.WriteLine();
        bool isValid = await VerifyIntegrityAsync(settings.SoftwarePath, product, serviceProvider);
        if (!isValid) {
            return 2;
        }

        AnsiConsole.WriteLine();
        var features = await AskModFeaturesForProductAsync(manifest, product);

        AnsiConsole.WriteLine();
        using Node? software = await ReadSoftwareAsync(settings.SoftwarePath, product, serviceProvider);
        if (software is null) {
            return 3;
        }

        AnsiConsole.WriteLine();
        bool installSuccess = await ApplyModResourcesAsync(software, mix, features, serviceProvider);
        if (!installSuccess) {
            return 4;
        }

        AnsiConsole.WriteLine();
        await Task.Delay(2_000);
        AnsiConsole.MarkupLine("Creating bundle... [green]TODO[/]");

        return 0;
    }

    private static MixPackage ReadMix(string modPath)
    {
        AnsiConsole.WriteLine("Reading MIX package");
        MixPackage mix = MixPackageReader.OpenRead(modPath);
        MixManifest manifest = mix.Manifest;
        AnsiConsole.MarkupLine("Reading MIX... [green]done[/]");

        string panelContent = $"{manifest.Mod.Description?.GetOrDefault("es_ES").EscapeMarkup()}\n"
            + $"[italic gray]by\n{manifest.Mod.Authors.EscapeMarkup()}[/]";
        var panel = new Panel(panelContent)
            .Header($"[bold blue]{manifest.Mod.Name} v{manifest.Mod.Version}[/]");
        AnsiConsole.Write(panel);

        return mix;
    }

    private static async Task<CompatibleProductInfo?> GetCompatibleProductAsync(
        MixManifest mix,
        string softwarePath,
        ModInstallerServiceProvider serviceProvider)
    {
        CompatibleProductInfo? product = null;
        foreach (var compatibleProduct in mix.Mod.CompatibleProducts) {
            AnsiConsole.MarkupLineInterpolated($"Checking match with {compatibleProduct.Name}");

            bool matchAllValidators = true;
            foreach (var verificationInfo in compatibleProduct.Verification ?? []) {
                var validator = serviceProvider.GetCompatibilityValidator(verificationInfo.Method);
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
        ModInstallerServiceProvider serviceProvider)
    {
        var integrity = serviceProvider.GetIntegrityValidator(product.Format);
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

    private static async Task<InstallationFeatures> AskModFeaturesForProductAsync(
        MixManifest mix,
        CompatibleProductInfo product)
    {
        var compatibleFeatures = mix.Resources
            .Where(r => r.CompatibleProducts.Any(c => c.ProductId == product.ProductId))
            .SelectMany(r => r.RequiredFeatures ?? [])
            .Select(g => g.FeatureId)
            .Distinct()
            .ToArray();

        var featuresInfo = mix.Mod.Features?
            .Where(f => compatibleFeatures.Contains(f.Id))
            .OrderBy(f => f.IsOptional)
            .ToArray();

        AnsiConsole.MarkupLine("[teal]Mod features[/]");
        InstallationFeatures selectedFeatures = [];
        foreach (var feature in featuresInfo ?? []) {
            AnsiConsole.Write(new Rule(feature.Name ?? feature.Id));
            if (feature.IsOptional) {
                bool include = await AnsiConsole.PromptAsync(
                    new TextPrompt<bool>("Optional feature. Add it?")
                        .AddChoices([true, false])
                        .DefaultValue(true)
                        .WithConverter(v => v ? "y" : "n"));

                if (!include) {
                    continue;
                }
            }
            else {
                AnsiConsole.MarkupLine("[green]Required[/] feature");
            }

            Dictionary<string, string> parameters = [];
            foreach (var parameter in feature.Parameters ?? []) {
                string value = await AnsiConsole.PromptAsync(
                    new TextPrompt<string>(parameter.Description ?? parameter.Id)
                        .DefaultValue(parameter.DefaultValue));
                parameters.Add(parameter.Id, value);
            }

            selectedFeatures[feature.Id] = parameters;
        }

        return selectedFeatures;
    }

    private static async Task<Node?> ReadSoftwareAsync(
        string softwarePath,
        CompatibleProductInfo product,
        ModInstallerServiceProvider serviceProvider)
    {
        AnsiConsole.WriteLine("Opening software");
        var reader = serviceProvider.GetSoftwareReader(product.Format);
        if (reader is null) {
            return null;
        }

        Node node = await reader.OpenPathAsync(softwarePath);
        AnsiConsole.MarkupLine("Software reading... [green]done![/]");

        return node;
    }

    private static async Task<bool> ApplyModResourcesAsync(
        Node software,
        MixPackage mix,
        InstallationFeatures features,
        ModInstallerServiceProvider serviceProvider)
    {
        var filteredResources = mix.Manifest.Resources
            .Where(r => r.RequiredFeatures.All(f => features.ContainsKey(f.FeatureId)));

        foreach (var resource in filteredResources) {
            var options = new ModInstallationOptions(resource.InstallParams ?? [], []);

            var installer = serviceProvider.GetResourceInstaller(resource.InstallStep);
            if (installer is null) {
                AnsiConsole.MarkupLineInterpolated($"[red]Cannot find installer for method: {resource.InstallStep}[/]");
                return false;
            }

            using var resourceData = mix.GetResource(resource.Content.Source);
            AnsiConsole.MarkupLineInterpolated($"Applying resources: [gray]{resource.Name}[/]");
            await installer.InstallResourceAsync(software, resourceData, options);
        }

        AnsiConsole.MarkupLine("Mod resources... [green]applied[/]");
        return true;
    }

    private sealed class InstallationFeatures : Dictionary<string, Dictionary<string, string>>
    {
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
