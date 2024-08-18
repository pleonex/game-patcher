namespace PleOps.GamePatcher.Poc.Pages.ModdingProject;

using System.IO;
using System.Reflection;
using System;
using Avalonia.Controls;
using PleOps.GamePatcher.Poc.ModdingProject;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

public partial class ModdingProjectInfoEditorView : UserControl
{
    public ModdingProjectInfoEditorView()
    {
        InitializeComponent();
    }
}

internal class ModdingProjectInfoEditorViewModelDesigner : ModdingProjectInfoEditorViewModel
{
    public ModdingProjectInfoEditorViewModelDesigner()
        : base(GetExampleManifest())
    {
    }

    private static Project GetExampleManifest()
    {
        string resourcePath = typeof(Program).Namespace + ".Assets.example.mdp.yml";
        Assembly assembly = typeof(Program).Assembly;
        using Stream exampleYaml = assembly.GetManifestResourceStream(resourcePath)
            ?? throw new InvalidOperationException("Cannot find example resource");

        using var reader = new StreamReader(exampleYaml);
        string manifestContent = reader.ReadToEnd();

        return new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build()
            .Deserialize<ModdingProjectManifest>(manifestContent)
            .Project;
    }
}
