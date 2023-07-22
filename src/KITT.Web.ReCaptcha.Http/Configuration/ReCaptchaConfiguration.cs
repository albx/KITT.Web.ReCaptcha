namespace KITT.Web.ReCaptcha.Http.Configuration;

public record ReCaptchaConfiguration
{
    public string SecretKey { get; set; } = string.Empty;
}
