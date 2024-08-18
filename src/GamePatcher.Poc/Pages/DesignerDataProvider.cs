namespace PleOps.GamePatcher.Poc.Pages;

using System;
using System.IO;
using System.Reflection;
using PleOps.GamePatcher.Poc.ModdingProject;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

internal static class DesignerDataProvider
{
    public static ModdingProjectManifest GetExampleModdingProjectManifest()
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
            .Deserialize<ModdingProjectManifest>(manifestContent);
    }
}
