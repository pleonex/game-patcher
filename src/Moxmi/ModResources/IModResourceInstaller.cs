namespace PleOps.Moxmi.ModResources;

using System.Threading.Tasks;
using Yarhl.FileSystem;

public interface IModResourceInstaller
{
    Task InstallResourceAsync(Node software, Stream resource, ModInstallationOptions options);
}
