namespace PleOps.Moxmi.Platforms.Ekona;

using PleOps.Moxmi.ModInstaller;
using PleOps.Moxmi.Platforms.Ekona.Compatibility;
using PleOps.Moxmi.Platforms.Ekona.Containers;
using PleOps.Moxmi.Platforms.Ekona.Integrity;

public static class EkonaPlatformProviderExtensions
{
    public static ModInstallerWorkflowProvider RegisterEkona(this ModInstallerWorkflowProvider provider)
    {
        provider.RegisterCompatibilityValidator("ds-gameid", new DsGameIdCompatibilityValidator());

        // TODO: register keys
        provider.RegisterIntegrityValidator("ds-rom", new NitroRomIntegrityValidator(null));
        provider.RegisterIntegrityValidator("dsi-rom", new NitroRomIntegrityValidator(null));

        provider.RegisterContainerConverter("ds-rom", new NitroRomContainerConverter());
        provider.RegisterContainerConverter("dsi-rom", new NitroRomContainerConverter()); // TODO: keys
        return provider;
    }
}
