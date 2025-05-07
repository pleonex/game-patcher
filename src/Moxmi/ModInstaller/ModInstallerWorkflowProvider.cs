namespace PleOps.Moxmi.ModInstaller;

using System;
using System.ComponentModel.DataAnnotations;
using PleOps.Moxmi.Compatibility;
using PleOps.Moxmi.Containers;
using PleOps.Moxmi.Integrity;
using Yarhl.FileFormat;

public class ModInstallerWorkflowProvider
{
    private readonly Dictionary<string, ICompatibilityValidator> compatibilityValidators;
    private readonly Dictionary<string, ISoftwareIntegrityValidator> integrityValidators;
    private readonly Dictionary<string, IContainerConverter> containerConverters;

    public ModInstallerWorkflowProvider()
    {
        compatibilityValidators = [];
        integrityValidators = [];
        containerConverters = [];

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

    public void RegisterContainerConverter(string format, IContainerConverter converter)
    {
        ArgumentException.ThrowIfNullOrEmpty(format);
        ArgumentNullException.ThrowIfNull(converter);

        containerConverters.Add(format, converter);
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

    public IContainerConverter GetContainerConverter(string softwareFormat)
    {
        ArgumentException.ThrowIfNullOrEmpty(softwareFormat);

        if (containerConverters.TryGetValue(softwareFormat, out var instance)) {
            return instance;
        }

        throw new NotSupportedException($"Unsupported software format: {softwareFormat}");
    }
}
