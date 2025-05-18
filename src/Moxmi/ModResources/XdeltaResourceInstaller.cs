namespace PleOps.Moxmi.ModResources;

using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PleOps.XdeltaSharp.Decoder;
using Yarhl.FileSystem;
using Yarhl.IO;

public class XdeltaResourceInstaller : IModResourceInstaller
{
    public Task InstallResourceAsync(Node software, Stream resource, ModInstallationJsonOptions options)
    {
        var xdeltaOpts = options.GetSection<XdeltaOptions>("xdelta");

        Node target = Navigator.SearchNode(software, xdeltaOpts.Path)
            ?? throw new InvalidOperationException("Cannot find target node");

        using var outputStream = new BinaryFormat();
        var decoder = new Decoder(target.Stream, resource, outputStream.Stream);
        decoder.Run();

        return Task.CompletedTask;
    }
}

public class XdeltaOptions
{
    [Required]
    public string Path { get; set; } = "";

    [Required]
    [JsonPropertyName("integrity_src")]
    public string IntegritySource { get; set; } = "";

    [Required]
    public string IntegrityTarget { get; set; } = "";
}
