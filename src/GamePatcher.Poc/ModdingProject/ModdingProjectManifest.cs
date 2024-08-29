namespace PleOps.GamePatcher.Poc.ModdingProject;

using System.Collections.ObjectModel;

public class ModdingProjectManifest
{
    public Project Project { get; set; } = new();
    public Collection<Product> Products { get; set; } =  [];
    public Collection<ModInfo> Mods { get; set; } = [];
    public Signature Signature { get; set; } = new();
}

public class Project
{
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public string Description { get; set; } = "";
    public Collection<Link> Links { get; set; } = [];
    public string Status { get; set; } = "";
    public string Team { get; set; } = "";
    public string Credits { get; set; } = "";
    public Logo Logo { get; set; } = new();
    public string AdditionalInformation { get; set; } = "";
    public Update Update { get; set; } = new();
}

public class Logo
{
    public string Icon { get; set; } = "";
    public string Large { get; set; } = "";
}

public class Update
{
    public string Method { get; set; } = "";
    public string Uri { get; set; } = "";
}

public class Link
{
    public string Name { get; set; } = "";
    public string Href { get; set; } = "";
}

public class Signature
{
    public string Certificate { get; set; } = "";
    public string Algorithm { get; set; } = "";
    public string Value { get; set; } = "";
}

public class Product
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Company { get; set; } = "";
    public string Publisher { get; set; } = "";
    public string ReleaseDate { get; set; } = "";
    public string Region { get; set; } = "";
    public string[] Genre { get; set; } = [];
    public string Platform { get; set; } = "";
    public string Description { get; set; } = "";
    public LinkedProducts LinkedProducts { get; set; } = new();
    public Collection<BuyInfo> BuyInfo { get; set; } = [];
}

public class LinkedProducts
{
    public Collection<RegionInfo> Regions { get; set; } = [];
    public Collection<SagaInfo> Saga { get; set; } = [];
}

public class RegionInfo
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Region { get; set; } = "";
}

public class SagaInfo
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
}

public class BuyInfo
{
    public string Store { get; set; } = "";
    public bool Discontinued { get; set; }
    public string Link { get; set; } = "";
    public string Comments { get; set; } = "";
}

public class ModInfo
{
    public string Id { get; set; } = "";
    public string Version { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public Screenshot[] Screenshots { get; set; } = [];
    public string TargetLanguage { get; set; } = "";
    public Compatibility[] Compatibility { get; set; } = [];
    public ModContent ModContent { get; set; } = new();
    public string ValidUntil { get; set; } = "";
    public Encryption Encryption { get; set; } = new();
}

public class Screenshot
{
    public string Href { get; set; } = "";

    public string Title { get; set; } = "";

    public string AltText { get; set; } = "";
}

public class ModContent
{
    public string Href { get; set; } = "";
    public string Format { get; set; } = "";
    public string HashAlgorithm { get; set; } = "";
    public string HashValue { get; set; } = "";
}

public class Encryption
{
    public ContentKey ContentKey { get; set; } = new();
    public UserKey UserKey { get; set; } = new();
}

public class ContentKey
{
    public string Algorithm { get; set; } = "";
    public string Key { get; set; } = "";
}

public class UserKey
{
    public string GenerationAlgorithm { get; set; } = "";
    public string KeyCheck { get; set; } = "";
    public string Hint { get; set; } = "";
}

public class Compatibility
{
    public string ProductId { get; set; } = "";
    public string Name { get; set; } = "";
    public Verification Verification { get; set; } = new();
}

public class Verification
{
    public string Method { get; set; } = "";
    public string Hash { get; set; } = "";
}
