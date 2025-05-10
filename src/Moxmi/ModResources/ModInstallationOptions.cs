namespace PleOps.Moxmi.ModResources;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// This class only works for YAML dictionaries so far, we could implement custom drivers of config deserializers
public class ModInstallationOptions(
    Dictionary<string, object> resourceParameters,
    Dictionary<string, string> productFeatureParameters)
{
    public T GetSection<T>(string key)
        where T : new()
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        if (!resourceParameters.TryGetValue(key, out object? valueObj)) {
            throw new InvalidOperationException("Missing key");
        }

        if (valueObj is not Dictionary<object, object> valueDict) {
            throw new NotSupportedException("Unsupported structure");
        }

        // To consider: Dictionary -> T it can be custom implement via interface
        T section = new T();
        var properties = GetFields(typeof(T));
        foreach (var configEntry in valueDict) {
            var propertyInfo = properties.FirstOrDefault(p => ComparePropertyName(p.Name, configEntry.Key))
                ?? throw new InvalidOperationException($"Missing property for name {configEntry.Key}");

            propertyInfo.SetValue(section, configEntry.Value);
        }

        return section;
    }

    private static bool ComparePropertyName(string propertyName, object configKey)
    {
        if (configKey is not string configKeyText) {
            return false;
        }

        configKeyText = configKeyText.Replace("_", null);
        return propertyName.Equals(configKeyText, StringComparison.InvariantCultureIgnoreCase);
    }

    private static PropertyInfo[] GetFields(Type type)
    {
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && (p.GetGetMethod(false)?.IsPublic ?? false))
            .Where(p => p.CanWrite && (p.GetSetMethod(false)?.IsPublic ?? false))
            .ToArray();

        return properties;
    }
}
