namespace PleOps.Moxmi.ModInstaller;

using System;
using System.Reflection;

public class ModInstallerWorkflowProvider
{
    private readonly IServiceProvider container;

    public ModInstallerWorkflowProvider()
    {
        // TODO: add dependency DI
    }

    public void RegisterPlatformExtension(Assembly assembly)
    {
        // Maybe another class being a wrapper over our container then build gives this one
    }

    public ICompatibilityValidator GetCompatibilityValidator(string method)
    {
        ArgumentException.ThrowIfNullOrEmpty(method);

        // this won't work, I need a way to identify them
        return method switch {
            "file-sha256" => (FileHashCompatibilityValidator)container.GetService(typeof(FileHashCompatibilityValidator))!,
            _ => throw new NotSupportedException($"Unsupported validator method: {method}"),
        };
    }
}
