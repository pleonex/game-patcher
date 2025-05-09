namespace PleOps.Moxmi.ModInstaller;

using System;
using PleOps.Moxmi.Compatibility;
using PleOps.Moxmi.Integrity;
using PleOps.Moxmi.ModResources;
using PleOps.Moxmi.Readers;

public class ModInstallerWorkflowProvider
{
    private readonly Dictionary<string, ICompatibilityValidator> compatibilityValidators;
    private readonly Dictionary<string, ISoftwareIntegrityValidator> integrityValidators;
    private readonly Dictionary<string, ISoftwareReader> readers;
    private readonly Dictionary<string, IModResourceInstaller> installers;

    public ModInstallerWorkflowProvider()
    {
        compatibilityValidators = [];
        integrityValidators = [];
        readers = [];
        installers = [];

        RegisterBuiltin();
    }

    private void RegisterBuiltin()
    {
        compatibilityValidators.Add("file-sha256", new FileHashCompatibilityValidator());
    }

    public void RegisterCompatibilityValidator(string method, ICompatibilityValidator validator)
    {
        ArgumentException.ThrowIfNullOrEmpty(method);
        ArgumentNullException.ThrowIfNull(validator);

        compatibilityValidators.Add(method, validator);
    }

    public void RegisterIntegrityValidator(string format, ISoftwareIntegrityValidator validator)
    {
        ArgumentException.ThrowIfNullOrEmpty(format);
        ArgumentNullException.ThrowIfNull(validator);

        integrityValidators.Add(format, validator);
    }

    public void RegisterSoftwareReader(string format, ISoftwareReader reader)
    {
        ArgumentException.ThrowIfNullOrEmpty(format);
        ArgumentNullException.ThrowIfNull(reader);

        readers.Add(format, reader);
    }

    public void RegisterModInstaller(string method, IModResourceInstaller installer)
    {
        ArgumentException.ThrowIfNullOrEmpty(method);
        ArgumentNullException.ThrowIfNull(installer);

        installers.Add(method, installer);
    }

    public ICompatibilityValidator? GetCompatibilityValidator(string method)
    {
        ArgumentException.ThrowIfNullOrEmpty(method);

        return compatibilityValidators.GetValueOrDefault(method);
    }

    public ISoftwareIntegrityValidator? GetIntegrityValidator(string softwareFormat)
    {
        ArgumentException.ThrowIfNullOrEmpty(softwareFormat);

        return integrityValidators.GetValueOrDefault(softwareFormat);
    }

    public ISoftwareReader? GetSoftwareReader(string softwareFormat)
    {
        ArgumentException.ThrowIfNullOrEmpty(softwareFormat);

        return readers.GetValueOrDefault(softwareFormat);
    }

    public IModResourceInstaller? GetResourceInstaller(string installerMethod)
    {
        ArgumentNullException.ThrowIfNull(installerMethod);

        return installers.GetValueOrDefault(installerMethod);
    }
}
