namespace KITT.Web.ReCaptcha.Sample.v2.Wasm;

public record VerifyResponse
{
    public IEnumerable<string> ErrorCodes { get; init; } = Enumerable.Empty<string>();

    public static VerifyResponse Success => new();

    public static VerifyResponse Failed(IEnumerable<string> errorCodes) => new() { ErrorCodes = errorCodes };
}
