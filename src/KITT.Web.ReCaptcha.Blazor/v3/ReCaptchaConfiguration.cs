namespace KITT.Web.ReCaptcha.Blazor.v3;

/// <summary>
/// Defines the configuration for reCaptcha v3 
/// </summary>
public record ReCaptchaConfiguration
{
    /// <summary>
    /// Gets or sets the client-side site key
    /// </summary>
    public string SiteKey { get; set; } = string.Empty;
}
