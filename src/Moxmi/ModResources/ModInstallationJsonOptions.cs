namespace PleOps.Moxmi.ModResources;

using System;
using System.Collections.Generic;
using System.Text.Json;

public class ModInstallationJsonOptions(
    Dictionary<string, object> resourceParameters,
    Dictionary<string, string> productFeatureParameters)
{
    private static readonly JsonSerializerOptions JsonOpts = new() {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true,
    };

    public T GetSection<T>(string key)
        where T : new()
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        if (!resourceParameters.TryGetValue(key, out object? valueObj)) {
            throw new InvalidOperationException("Missing key");
        }

        if (valueObj is not JsonElement jsonElement) {
            throw new NotSupportedException("Unsupported structure");
        }

        // TODO: apply feature replacements
        // TODO: do model validation
        return JsonSerializer.Deserialize<T>(jsonElement, JsonOpts)
            ?? throw new InvalidOperationException("Invalid structure");
    }
}
