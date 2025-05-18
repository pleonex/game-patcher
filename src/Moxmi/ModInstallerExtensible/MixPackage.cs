namespace PleOps.Moxmi.ModInstallerExtensible;

using System;

public sealed class MixPackage : IDisposable
{
    private readonly IMixPackageReader reader;
    private readonly Lazy<MixManifest> lazyManifest;

    internal MixPackage(IMixPackageReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);

        this.reader = reader;
        lazyManifest = new Lazy<MixManifest>(reader.GetManifest);
    }

    public MixManifest Manifest => lazyManifest.Value;

    public static MixPackage FromZipFile(string zipPath)
    {
        var reader = new MixPackageReader(zipPath);
        return new MixPackage(reader);
    }

    public Stream GetResource(string uri)
    {
        ArgumentException.ThrowIfNullOrEmpty(uri);
        return reader.GetResource(uri);
    }

    public void Dispose()
    {
        (reader as IDisposable)?.Dispose();
    }
}
