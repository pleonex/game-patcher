namespace PleOps.Moxmi.Containers;

using Yarhl.FileFormat;
using Yarhl.FileSystem;
using Yarhl.IO;

public interface IContainerConverter
    : IConverter<IBinary, NodeContainerFormat>,
    IConverter<NodeContainerFormat, IBinary>
{
}
