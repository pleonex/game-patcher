namespace PleOps.Moxmi.ModInstallerExtensible;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using YamlDotNet.Serialization;

public class MixManifest
{
    public ProjectInfo Project { get; set; } = new();

    public ModInfo Mod { get; set; } = new();

    public Collection<Resource> Resources { get; set; } = [];

    public InstallationInfo Installation { get; set; } = new();

    public Signature? Signature { get; set; }
}

public class ProjectInfo
{
    [Required]
    public string Name { get; set; } = "";

    [DeniedValues(ProjectKind.None)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProjectKind Type { get; set; }

    public LocalizedText? Description { get; set; } = [];

    public Collection<Link>? Links { get; set; } = [];

    [DeniedValues(ProjectStatus.None)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProjectStatus Status { get; set; }

    public string? Team { get; set; }

    public string? Credits { get; set; }

    public Logo? Logo { get; set; } = new();

    public string? AdditionalInformation { get; set; }
}

public enum ProjectKind
{
    None,
    Translation,
    Other,
}

public enum ProjectStatus
{
    None,
    InProgress,
    Finished,
    Paused,
    Cancelled,
}

public class LocalizedText : Dictionary<string, string>
{
    private const string DefaultLanguageCode = "en";

    // Rename conflict with getvalueordefault
    public string GetOrDefault(string languageCode)
    {
        if (TryGetValue(languageCode, out string? text)) {
            return text;
        }

        return this[DefaultLanguageCode];
    }
}

public class Logo
{
    public Subresource? Icon { get; set; }

    public Subresource? Large { get; set; }
}

public class Subresource
{
    [Required]
    [YamlMember(Alias = "src")]
    [JsonPropertyName("src")]
    public string Source { get; set; } = "";

    [Required]
    public string Integrity { get; set; } = "";
}

public class Link
{
    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Href { get; set; } = "";
}

public class ModInfo
{
    [Required]
    public string Id { get; set; } = "";

    [Required]
    public string Version { get; set; } = "";

    [Required]
    public string Name { get; set; } = "";

    public string? Authors { get; set; }

    public LocalizedText? Description { get; set; } = [];

    public string? TargetLanguage { get; set; }

    [Required]
    [MinLength(1)]
    public Collection<CompatibleProductInfo> CompatibleProducts { get; set; } = [];

    [Required]
    [MinLength(1)]
    public Collection<ModFeatureInfo> Features { get; set; } = [];
}

public class CompatibleProductInfo
{
    [Required]
    public string ProductId { get; set; } = "";

    public string? Name { get; set; }

    public string Platform { get; set; } = "";

    [Required]
    public string Format { get; set; } = "";

    public Collection<VerificationMethodInfo>? Verification { get; set; } = [];
}

public class VerificationMethodInfo
{
    [Required]
    public string Method { get; set; } = "";

    [Required]
    public string Value { get; set; } = "";
}

public class ModFeatureInfo
{
    [Required]
    public string Id { get; set; } = "";

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool IsOptional { get; set; }

    public Collection<ModFeatureParameter>? Parameters { get; set; } = [];
}

public class ModFeatureParameter
{
    [Required]
    public string Id { get; set; } = "";

    public string? Description { get; set; }

    [Required]
    [YamlMember(Alias = "default")]
    [JsonPropertyName("default")]
    public string DefaultValue { get; set; } = "";

    [Required]
    public string Type { get; set; } = "";
}

public class InstallationInfo
{
    [Required]
    public string Method { get; set; } = "simple"; // only method supported for now

    // Future usage
    public Collection<Dictionary<string, object>>? Matrix { get; set; }

    [Required]
    [MinLength(1)]
    public Collection<DeploymentInfo> Deployment { get; set; } = [];
}

public class DeploymentInfo
{
    [Required]
    public string Name { get; set; } = "";

    public Dictionary<string ,object>? Parameters { get; set; } = [];
}

public class Signature
{
    [Required]
    public string Certificate { get; set; } = "";

    [Required]
    public string Algorithm { get; set; } = "";

    [Required]
    public string Value { get; set; } = "";
}

public class Resource
{
    [Required]
    public string Id { get; set; } = "";

    public string? Name { get; set; }

    [Required]
    public string InstallStep { get; set; } = "";

    [Required]
    public Subresource Content { get; set; } = new();

    [Required]
    [MinLength(1)]
    public Collection<ResourceCompatibleProduct> CompatibleProducts { get; set; } = [];

    [Required]
    [MinLength(1)]
    public Collection<ResourceFeatureInfo> RequiredFeatures { get; set; } = [];

    public Dictionary<string ,object>? InstallParams { get; set; } = [];
}

public class ResourceCompatibleProduct
{
    [Required]
    public string ProductId { get; set; } = "";
}

public class ResourceFeatureInfo
{
    [Required]
    public string FeatureId { get; set; } = "";
}
