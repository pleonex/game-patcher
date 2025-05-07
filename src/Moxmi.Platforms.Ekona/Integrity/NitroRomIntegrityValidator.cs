namespace PleOps.Moxmi.Platforms.Ekona.Integrity;

using System;
using System.Threading.Tasks;
using PleOps.Moxmi.Integrity;
using SceneGate.Ekona.Containers.Rom;
using SceneGate.Ekona.Security;
using Yarhl.IO;

public class NitroRomIntegrityValidator(DsiKeyStore? dsiKeys)
    : ISoftwareIntegrityValidator
{
    private readonly DsiKeyStore? dsiKeys = dsiKeys;

    public Task<SoftwareIntegrityStatus> VerifyIntegrityAsync(string softwarePath)
    {
        NitroRom rom;
        try {
            using var binarySoftware = new BinaryFormat(softwarePath, FileOpenMode.Read);
            rom = new Binary2NitroRom(dsiKeys).Convert(binarySoftware);
        } catch (Exception) {
            return Task.FromResult(SoftwareIntegrityStatus.Corrupted);
        }

        if (dsiKeys is null) {
            bool isNonKeyHashesValid = rom.Information.ChecksumHeader.Status is HashStatus.Valid
                && rom.Information.ChecksumLogo.Status is HashStatus.Valid;
            var dataValidity = isNonKeyHashesValid
                ? IntegrityVerificationResult.CannotValidate
                : IntegrityVerificationResult.Invalid;

            var signatureResult = rom.Information.UnitCode is DeviceUnitKind.DS
                ? IntegrityVerificationResult.Valid
                : IntegrityVerificationResult.CannotValidate;

            var dsResult = new SoftwareIntegrityStatus(dataValidity, signatureResult);
            return Task.FromResult(dsResult);
        }

        var status = new SoftwareIntegrityStatus(
            rom.Information.HasValidHashes().ToIntegrityStatus(),
            rom.Information.HasValidSignature().ToIntegrityStatus());

        return Task.FromResult(status);
    }
}
