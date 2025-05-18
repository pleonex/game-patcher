namespace PleOps.Moxmi.ModInstaller;

using System;
using System.IO;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using System.Text.Json;

public class ModInstallerExtensibleSerializer
{
    private static readonly JsonSerializerOptions JsonOpts = new() {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true,
    };

    public ModInstallerExtensibleManifest DeserializeJson(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        return JsonSerializer.Deserialize<ModInstallerExtensibleManifest>(stream, JsonOpts)
            ?? throw new InvalidOperationException("Cannot deserialize MIX manifest");
    }

    public ModInstallerExtensibleManifest DeserializeJson(string content)
    {
        ArgumentException.ThrowIfNullOrEmpty(content);

        return JsonSerializer.Deserialize<ModInstallerExtensibleManifest>(content, JsonOpts)
            ?? throw new InvalidOperationException("Cannot deserialize MIX manifest");
    }

    public ModInstallerExtensibleManifest DeserializeYaml(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        using var reader = new StreamReader(stream);
        string content = reader.ReadToEnd();

        return DeserializeYaml(content);
    }

    public ModInstallerExtensibleManifest DeserializeYaml(string content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(content);

        return new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build()
            .Deserialize<ModInstallerExtensibleManifest>(content);
    }
}
