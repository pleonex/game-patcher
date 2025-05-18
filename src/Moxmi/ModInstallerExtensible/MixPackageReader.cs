namespace PleOps.Moxmi.ModInstallerExtensible;

using System.IO;
using System.IO.Compression;

public static class MixPackageReader
{
    private const string ManifestPath = "manifest.json";

    public static MixPackage OpenRead(string packagePath)
    {
        ZipArchive package = ZipFile.OpenRead(packagePath);

        // TODO: verify version.json for format
        using Stream manifestData = package.GetEntry(ManifestPath)?.Open()
            ?? throw new FileNotFoundException("Missing manifest file", ManifestPath);
        var manifest = MixManifestSerializer.DeserializeJson(manifestData);

        return new MixPackage(manifest, package);
    }
}
