namespace PleOps.Moxmi.Integrity;

public record SoftwareIntegrityStatus(
    IntegrityVerificationResult IsDataValid,
    IntegrityVerificationResult HasValidPublisherSignature)
{
    public static SoftwareIntegrityStatus Corrupted => new(
        IsDataValid: IntegrityVerificationResult.Invalid,
        HasValidPublisherSignature: IntegrityVerificationResult.CannotValidate);
}
