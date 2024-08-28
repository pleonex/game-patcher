namespace PleOps.GamePatcher.Poc.XamlExtensions;

using Avalonia.Data.Converters;

public static class NumberConverters
{
    public static readonly IValueConverter IsGreaterThan =
        new FuncValueConverter<double, string, bool>((v, p) => v > int.Parse(p ?? "0"));

    public static readonly IValueConverter IsLessThan =
        new FuncValueConverter<double, string, bool>((v, p) => v < int.Parse(p ?? "0"));
}
