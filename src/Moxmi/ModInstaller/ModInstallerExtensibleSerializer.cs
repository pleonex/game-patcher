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

    public MixManifest DeserializeJson(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        // TODO: model validation
        return JsonSerializer.Deserialize<MixManifest>(stream, JsonOpts)
            ?? throw new InvalidOperationException("Cannot deserialize MIX manifest");
    }

    public MixManifest DeserializeJson(string content)
    {
        ArgumentException.ThrowIfNullOrEmpty(content);

        return JsonSerializer.Deserialize<MixManifest>(content, JsonOpts)
            ?? throw new InvalidOperationException("Cannot deserialize MIX manifest");
    }

    public MixManifest DeserializeYaml(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        using var reader = new StreamReader(stream);
        string content = reader.ReadToEnd();

        return DeserializeYaml(content);
    }

    public MixManifest DeserializeYaml(string content)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(content);

        return new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build()
            .Deserialize<MixManifest>(content);
    }
}
