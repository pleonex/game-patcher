namespace PleOps.Moxmi.Platforms.Ekona;

using System;
using System.Threading.Tasks;
using PleOps.Moxmi.ModInstaller;
using SceneGate.Ekona.Containers.Rom;
using Yarhl.IO;

public class DSiRomIntegrityValidator : ISoftwareIntegrityValidator
{
    public Task<bool> VerifyIntegrity(string softwarePath)
    {
        // TODO: this needs keys or validate the hashes that doesn't need them (DI?)
        NitroRom rom;
        try {
            using var binarySoftware = new BinaryFormat(softwarePath, FileOpenMode.Read);
            rom = new Binary2NitroRom().Convert(binarySoftware);
        } catch (Exception) {
            return Task.FromResult(false);
        }

        bool validHashes = rom.Information.HasValidHashes();
        bool validSignatures = rom.Information.HasValidSignature();
        return Task.FromResult(validHashes && validSignatures);
    }
}
