namespace PleOps.Moxmi.Packages;

using Yarhl.FileSystem;

public interface ISoftwareReader
{
    Task<Node> OpenPathAsync(string softwarePath);
}
