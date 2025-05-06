namespace PleOps.Moxmi.Platforms.Ekona;

using PleOps.Moxmi.ModInstaller;
using PleOps.Moxmi.Platforms.Ekona.Integrity;

public static class EkonaPlatformProviderExtensions
{
    public static ModInstallerWorkflowProvider RegisterEkona(this ModInstallerWorkflowProvider provider)
    {
        // TODO: register keys
        provider.RegisterIntegrityValidator("ds-rom", new DSiRomIntegrityValidator(null));
        provider.RegisterIntegrityValidator("dsi-rom", new DSiRomIntegrityValidator(null));
        return provider;
    }
}
