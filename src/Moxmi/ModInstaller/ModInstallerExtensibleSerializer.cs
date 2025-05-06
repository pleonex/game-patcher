namespace PleOps.Moxmi.ModInstaller;

using System;
using System.IO;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

public class ModInstallerExtensibleSerializer
{
    public ModInstallerExtensibleManifest Deserialize(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        using var reader = new StreamReader(stream);
        string content = reader.ReadToEnd();

        return Deserialize(content);
    }

    public ModInstallerExtensibleManifest Deserialize(string content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(content);

        return new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build()
            .Deserialize<ModInstallerExtensibleManifest>(content);
    }
}
