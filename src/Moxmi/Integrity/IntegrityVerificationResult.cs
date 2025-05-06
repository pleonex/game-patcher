namespace PleOps.Moxmi.Integrity;

public enum IntegrityVerificationResult
{
    /// <summary>
    /// The integrity operation can't be performed (e.g. missing keys).
    /// </summary>
    CannotValidate,

    /// <summary>
    /// The integrity has been validated and is valid.
    /// </summary>
    Valid,

    /// <summary>
    /// The integrity has been validated and is not valid.
    /// </summary>
    Invalid,
}

public static class IntegrityVerificationResultExtensions
{
    public static IntegrityVerificationResult ToIntegrityStatus(this bool isValid)
    {
        return isValid ? IntegrityVerificationResult.Valid : IntegrityVerificationResult.Invalid;
    }
}
