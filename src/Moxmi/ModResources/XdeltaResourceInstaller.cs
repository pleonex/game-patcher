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
    public async Task InstallResourceAsync(Node software, Stream resource, ModInstallationOptions options)
    {
        var xdeltaOpts = options.GetSection<XdeltaOptions>("xdelta");

        Node target = Navigator.SearchNode(software, xdeltaOpts.Path)
            ?? throw new InvalidOperationException("Cannot find target node");

        if (target.Stream is null) {
            throw new InvalidOperationException("The target node doesn't have binary format");
        }

        // xdelta decoder needs to compare the current Position and Length
        // but the deflatestream doesn't support these properties.
        // TEMPORARILY we create a copy on memory, ideally on disk to extract
        using var resourceCopy = new MemoryStream();
        await resource.CopyToAsync(resourceCopy);

        var outputStream = new BinaryFormat();
        resourceCopy.Position = 0;
        target.Stream.Position = 0;
        var decoder = new Decoder(target.Stream, resourceCopy, outputStream.Stream);
        decoder.Run();

        target.ChangeFormat(outputStream);
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
