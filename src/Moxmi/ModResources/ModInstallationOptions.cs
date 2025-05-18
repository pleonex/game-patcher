namespace PleOps.Moxmi.ModResources;

using System;
using System.Collections.Generic;
using System.Text.Json;

public class ModInstallationOptions(
    Dictionary<string, object> resourceParameters,
    Dictionary<string, string> productFeatureParameters)
{
    private static readonly JsonSerializerOptions JsonOpts = new() {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true,
    };

    public virtual T GetSection<T>(string key)
        where T : new()
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        if (!resourceParameters.TryGetValue(key, out object? valueObj)) {
            throw new InvalidOperationException("Missing key");
        }

        if (valueObj is not JsonElement jsonElement) {
            throw new NotSupportedException("Unsupported structure");
        }

        // TODO: apply feature replacements via JSON type info modifiers
        // TODO: do model validation
        return jsonElement.Deserialize<T>(JsonOpts)
            ?? throw new InvalidOperationException("Invalid structure");
    }
}
