namespace KITT.Web.ReCaptcha.Blazor.v3;

public record ReCaptchaResult
{
    public bool Succeeded { get; private set; }

    public string Response { get; private set; } = string.Empty;

    public string Error { get; private set; } = string.Empty;

    private ReCaptchaResult() { }

    public static ReCaptchaResult Success(string response)
        => new() { Succeeded = true, Response = response };

    public static ReCaptchaResult Failed(string error)
        => new() { Succeeded = false, Error = error };
}
