namespace PleOps.Moxmi.ModInstaller;

using System;
using PleOps.Moxmi.Compatibility;
using PleOps.Moxmi.Integrity;

public class ModInstallerWorkflowProvider
{
    private readonly Dictionary<string, ICompatibilityValidator> compatibilityValidators;
    private readonly Dictionary<string, ISoftwareIntegrityValidator> integrityValidators;

    public ModInstallerWorkflowProvider()
    {
        compatibilityValidators = [];
        integrityValidators = [];

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

    public ICompatibilityValidator GetCompatibilityValidator(string method)
    {
        ArgumentException.ThrowIfNullOrEmpty(method);

        if (compatibilityValidators.TryGetValue(method, out var instance)) {
            return instance;
        }

        throw new NotSupportedException($"Unsupported validator method: {method}");
    }

    public ISoftwareIntegrityValidator GetIntegrityValidator(string softwareFormat)
    {
        ArgumentException.ThrowIfNullOrEmpty(softwareFormat);

        if (integrityValidators.TryGetValue(softwareFormat, out var instance)) {
            return instance;
        }

        throw new NotSupportedException($"Unsupported software format: {softwareFormat}");
    }
}
