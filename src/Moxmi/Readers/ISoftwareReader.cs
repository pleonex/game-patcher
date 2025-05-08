namespace PleOps.Moxmi.Readers;

using Yarhl.FileSystem;

public interface ISoftwareReader
{
    Task<Node> OpenPathAsync(string softwarePath);
}
