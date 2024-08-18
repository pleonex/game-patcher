namespace PleOps.GamePatcher.Poc.ModdingProject;

using System;
using System.IO;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

public class ModdingProjectManifestSerializer
{
    public ModdingProjectManifest Deserialize(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        using var reader = new StreamReader(stream);
        string content = reader.ReadToEnd();

        return Deserialize(content);
    }

    public ModdingProjectManifest Deserialize(string content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(content);

        return new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build()
            .Deserialize<ModdingProjectManifest>(content);
    }
}
