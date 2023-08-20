namespace KITT.Web.ReCaptcha.Http.Configuration;

/// <summary>
/// Contains the configuration options for the reCaptcha server side validation
/// </summary>
public record ReCaptchaConfiguration
{
    /// <summary>
    /// Gets or sets the server-side secret key used to verify the user response
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;
}
