namespace PleOps.Moxmi.Platforms.Ekona.Containers;

using PleOps.Moxmi.Containers;
using SceneGate.Ekona.Containers.Rom;
using Yarhl.FileSystem;
using Yarhl.IO;

public class NitroRomContainerConverter : IContainerConverter
{
    public NodeContainerFormat Convert(IBinary source)
    {
        return new Binary2NitroRom().Convert(source);
    }

    public IBinary Convert(NodeContainerFormat source)
    {
        // TODO: large streams?
        return new NitroRom2Binary().Convert(source);
    }
}
