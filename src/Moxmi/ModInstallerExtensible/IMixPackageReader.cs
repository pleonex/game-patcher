namespace PleOps.Moxmi.ModInstallerExtensible;

internal interface IMixPackageReader
{
    MixManifest GetManifest();

    Stream GetResource(string uri);
}
