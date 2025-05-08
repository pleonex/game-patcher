namespace PleOps.Moxmi.ModInstaller;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using YamlDotNet.Serialization;

// TODO: use nullable reference without default values, required
// and set attributes for validation
public class ModInstallerExtensibleManifest
{
    public ProjectInfo Project { get; set; } = new();
    public ModInfo Mod { get; set; } = new();
    public Collection<Resource> Resources { get; set; } = [];
    public Signature Signature { get; set; } = new();
}

public class ProjectInfo
{
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public Dictionary<string ,string> Description { get; set; } = [];
    public Collection<Link> Links { get; set; } = [];
    public string Status { get; set; } = "";
    public string Team { get; set; } = "";
    public string Credits { get; set; } = "";
    public Logo Logo { get; set; } = new();
    public string? AdditionalInformation { get; set; }
}

public class Logo
{
    public string Icon { get; set; } = "";
    public string Large { get; set; } = "";
}

public class Link
{
    public string Name { get; set; } = "";
    public string Href { get; set; } = "";
}

public class ModInfo
{
    public string Id { get; set; } = "";
    public string Version { get; set; } = "";
    public string Name { get; set; } = "";
    public string Authors { get; set; } = "";
    public string Description { get; set; } = "";
    public string TargetLanguage { get; set; } = "";
    public Collection<CompatibleProductInfo> Compatibility { get; set; } = [];
    public Collection<FeatureGroup> FeatureGroups { get; set; } = [];
    public Collection<Deployment> Deployment { get; set; } = [];
}

public class CompatibleProductInfo
{
    public string ProductId { get; set; } = "";
    public string Name { get; set; } = "";
    public string Platform { get; set; } = "";
    public string Format { get; set; } = "";
    public Collection<VerificationMethodInfo> Verification { get; set; } = [];
}

public class VerificationMethodInfo
{
    public string Method { get; set; } = "";
    public string Value { get; set; } = "";
}

public class FeatureGroup
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsOptional { get; set; }
    public Collection<FeatureGroupParameter> Parameters { get; set; } = [];
}

public class FeatureGroupParameter
{
    public string Id { get; set; } = "";
    public string Description { get; set; } = "";

    [YamlMember(Alias = "default")]
    public string DefaultValue { get; set; } = "";
    public string Type { get; set; } = "";
}

public class Deployment
{
    public string Name { get; set; } = "";
    public Dictionary<string ,string> Parameters { get; set; } = [];
}

public class Signature
{
    public string Certificate { get; set; } = "";
    public string Algorithm { get; set; } = "";
    public string Value { get; set; } = "";
}

public class Resource
{
    public string Id { get; set; } = "";
    public string InstallationMethod { get; set; } = "";
    public Content Content { get; set; } = new();
    public Collection<ResourceCompatibleProduct> Compatibility { get; set; } = [];
    public ResourceFeatureGroup[] FeatureGroup { get; set; } = [];
    public Dictionary<string ,object> InstallationParameters { get; set; } = [];
}

public class Content
{
    public string Href { get; set; } = "";
    public string Format { get; set; } = "";
    public string HashAlgorithm { get; set; } = "";
    public string HashValue { get; set; } = "";
}

public class ResourceCompatibleProduct
{
    public string ProductId { get; set; } = "";
}

public class ResourceFeatureGroup
{
    public string Name { get; set; } = "";
    public int Order { get; set; }
    public bool IsOptional { get; set; }
}
