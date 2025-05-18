namespace PleOps.Moxmi.Platforms.Ekona.Packages;

using SceneGate.Ekona.Containers.Rom;
using Yarhl.FileSystem;
using Yarhl.IO;

public class NitroRomWriter
{
    public void WriteToFile(Node software, string outputPath)
    {
        using var outputStream = DataStreamFactory.FromFile(outputPath, FileOpenMode.ReadWrite);
        var parameters = new NitroRom2BinaryParams {
            OutputStream = outputStream,
        };

        var converter = new NitroRom2Binary(parameters);
        converter.Convert(software.GetFormatAs<NodeContainerFormat>()!);
    }
}
