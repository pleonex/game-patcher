namespace PleOps.Moxmi.ModInstaller;

using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

public class FileHashCompatibilityValidator : ICompatibilityValidator
{
    public async Task<bool> VerifyCompatibilityAsync(string softwarePath, string compatibilityValue)
    {
        using FileStream data = File.OpenRead(softwarePath);
        byte[] hash = await SHA256.HashDataAsync(data);

        string textHash = Convert.ToHexString(hash);
        return textHash.Equals(compatibilityValue, StringComparison.InvariantCultureIgnoreCase);
    }
}
