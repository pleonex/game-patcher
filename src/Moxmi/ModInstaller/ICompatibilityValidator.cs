namespace PleOps.Moxmi.ModInstaller;

using System.Threading.Tasks;

public interface ICompatibilityValidator
{
    Task<bool> VerifyCompatibilityAsync(string softwarePath, string compatibilityValue);
}
