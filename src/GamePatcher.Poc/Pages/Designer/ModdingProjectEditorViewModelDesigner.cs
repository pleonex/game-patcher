namespace PleOps.GamePatcher.Poc.Pages.Designer;

using System;
using System.IO;
using System.Reflection;
using PleOps.GamePatcher.Poc.ModdingProject;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

internal class ModdingProjectEditorViewModelDesigner : ModdingProjectEditorViewModel
{
    public ModdingProjectEditorViewModelDesigner()
        : base(GetExampleManifest())
    {
    }

    private static ModdingProjectManifest GetExampleManifest()
    {
        string resourcePath = typeof(Program).Namespace + ".Assets.example.mdp.yml";
        Assembly assembly = typeof(ModdingProjectEditorViewModelDesigner).Assembly;
        using Stream exampleYaml = assembly.GetManifestResourceStream(resourcePath)
            ?? throw new InvalidOperationException("Cannot find example resource");

        using var reader = new StreamReader(exampleYaml);
        string manifestContent = reader.ReadToEnd();

        return new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build()
            .Deserialize<ModdingProjectManifest>(manifestContent);
    }
}
