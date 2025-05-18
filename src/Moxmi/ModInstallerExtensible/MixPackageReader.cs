namespace PleOps.Moxmi.ModInstallerExtensible;

using System.IO;
using System.IO.Compression;

internal class MixPackageReader : IMixPackageReader
{
    private const string ManifestPath = "manifest.json";

    private readonly ZipArchive package;

    public MixPackageReader(string packagePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(packagePath);

        // TODO: verify version.json for format
        package = ZipFile.OpenRead(packagePath);
    }

    public MixManifest GetManifest()
    {
        using Stream manifestData = package.GetEntry(ManifestPath)?.Open()
            ?? throw new FileNotFoundException("Missing manifest file", ManifestPath);
        var manifest = MixManifestSerializer.DeserializeJson(manifestData);
        return manifest;
    }

    public Stream GetResource(string uri)
    {
        ArgumentException.ThrowIfNullOrEmpty(uri);
        if (!uri.StartsWith("content://", StringComparison.InvariantCultureIgnoreCase)) {
            throw new NotSupportedException("Path URI not supported");
        }

        string path = uri.Substring("content://".Length);

        // TODO: accept integrity verification
        return package.GetEntry(path)?.Open()
            ?? throw new FileNotFoundException("Resource not in package", path);
    }
}
