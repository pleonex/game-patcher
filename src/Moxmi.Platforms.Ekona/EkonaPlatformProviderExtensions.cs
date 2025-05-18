namespace PleOps.Moxmi.Platforms.Ekona;

using PleOps.Moxmi;
using PleOps.Moxmi.Platforms.Ekona.Compatibility;
using PleOps.Moxmi.Platforms.Ekona.Integrity;
using PleOps.Moxmi.Platforms.Ekona.Readers;

public static class EkonaPlatformProviderExtensions
{
    public static ModInstallerServiceProvider RegisterEkona(this ModInstallerServiceProvider provider)
    {
        provider.RegisterCompatibilityValidator("ds-gameid", new DsGameIdCompatibilityValidator());

        // TODO: register keys
        provider.RegisterIntegrityValidator("ds-rom", new NitroRomIntegrityValidator(null));
        provider.RegisterIntegrityValidator("dsi-rom", new NitroRomIntegrityValidator(null));

        provider.RegisterSoftwareReader("ds-rom", new NitroRomReader());
        provider.RegisterSoftwareReader("dsi-rom", new NitroRomReader());
        return provider;
    }
}
