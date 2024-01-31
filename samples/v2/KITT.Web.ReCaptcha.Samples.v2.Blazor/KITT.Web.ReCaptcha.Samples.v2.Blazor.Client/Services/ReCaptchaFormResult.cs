namespace KITT.Web.ReCaptcha.Samples.v2.Blazor.Client.Services;

public record ReCaptchaFormResult
{
    public string? ErrorMessage { get; set; }

    public string[] ErrorCodes { get; set; } = [];


    public static ReCaptchaFormResult Succeeded => new();

    public static ReCaptchaFormResult Failed(string errorMessage, string[] errorCodes)
        => new() { ErrorMessage = errorMessage, ErrorCodes = errorCodes };
}
