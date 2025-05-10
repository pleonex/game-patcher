namespace PleOps.Moxmi.ModResources;

using System.IO;
using System.Threading.Tasks;
using PleOps.XdeltaSharp.Decoder;
using Yarhl.FileSystem;
using Yarhl.IO;

public class XdeltaResourceInstaller : IModResourceInstaller
{
    public Task InstallResourceAsync(Node software, Stream resource, ModInstallationYamlOptions options)
    {
        var xdeltaOpts = options.GetSection<XdeltaOptions>("xdelta");

        Node target = Navigator.SearchNode(software, xdeltaOpts.Target!)
            ?? throw new InvalidOperationException("Cannot find target node");

        using var outputStream = new BinaryFormat();
        var decoder = new Decoder(target.Stream, resource, outputStream.Stream);
        decoder.Run();

        return Task.CompletedTask;
    }
}

public class XdeltaOptions
{
    // TODO: do model validation
    public string? Target { get; set; }

    public string? OriginalHash { get; set; }

    public string? ModifiedHash { get; set; }
}
