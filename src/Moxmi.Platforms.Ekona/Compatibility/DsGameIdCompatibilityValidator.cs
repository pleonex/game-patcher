namespace PleOps.Moxmi.Platforms.Ekona.Compatibility;

using System.Threading.Tasks;
using PleOps.Moxmi.Compatibility;
using SceneGate.Ekona.Containers.Rom;
using Yarhl.IO;

public class DsGameIdCompatibilityValidator : ICompatibilityValidator
{
    public Task<bool> VerifyCompatibilityAsync(string softwarePath, string compatibilityValue)
    {
        // We only need the header, no need to parse the full ROM
        using var softwareData = new BinaryFormat(softwarePath, FileOpenMode.Read);
        RomHeader header = new Binary2RomHeader().Convert(softwareData);

        ProgramInfo info = header.ProgramInfo;
        string gameId = $"{info.GameCode}-{info.MakerCode}-{info.Version:D2}";

        return Task.FromResult(gameId == compatibilityValue);
    }
}
