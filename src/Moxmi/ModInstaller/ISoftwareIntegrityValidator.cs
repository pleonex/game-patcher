namespace PleOps.Moxmi.ModInstaller;

using System.Threading.Tasks;

public interface ISoftwareIntegrityValidator
{
    Task<bool> VerifyIntegrity(string softwarePath);
}
