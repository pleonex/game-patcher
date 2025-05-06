namespace PleOps.Moxmi.Integrity;

using System.Threading.Tasks;

public interface ISoftwareIntegrityValidator
{
    Task<SoftwareIntegrityStatus> VerifyIntegrityAsync(string softwarePath);
}
