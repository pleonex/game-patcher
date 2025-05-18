namespace PleOps.Moxmi.ModInstallerExtensible;

using System;
using System.IO.Compression;

public sealed class MixPackage : IDisposable
{
    private readonly ZipArchive archive;

    internal MixPackage(MixManifest manifest, ZipArchive archive)
    {
        ArgumentNullException.ThrowIfNull(manifest);
        ArgumentNullException.ThrowIfNull(archive);

        this.archive = archive;
        Manifest = manifest;
    }

    public MixManifest Manifest { get; }

    public Stream GetResource(string uri)
    {
        ArgumentException.ThrowIfNullOrEmpty(uri);
        if (!uri.StartsWith("content://", StringComparison.InvariantCultureIgnoreCase)) {
            throw new NotSupportedException("Path URI not supported");
        }

        string path = uri.Substring("content://".Length);

        // TODO: accept integrity verification
        return archive.GetEntry(path)?.Open()
            ?? throw new FileNotFoundException("Resource not in package", path);
    }

    public void Dispose()
    {
        archive.Dispose();
    }
}
