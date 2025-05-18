namespace PleOps.Moxmi.Platforms.Ekona.Readers;

using PleOps.Moxmi.Readers;
using SceneGate.Ekona.Containers.Rom;
using Yarhl.FileSystem;
using Yarhl.IO;

public class NitroRomReader : ISoftwareReader
{
    public Task<Node> OpenPathAsync(string softwarePath)
    {
        Node node = NodeFactory.FromFile(softwarePath, "root", FileOpenMode.Read)
            .TransformWith<Binary2NitroRom>();
        return Task.FromResult(node);
    }
}
