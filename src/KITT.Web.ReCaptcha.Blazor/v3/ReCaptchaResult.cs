namespace KITT.Web.ReCaptcha.Blazor.v3;

/// <summary>
/// Describes the reCaptcha validation result
/// </summary>
public record ReCaptchaResult
{
    /// <summary>
    /// Gets whether the reCaptcha validation succeed
    /// </summary>
    public bool Succeeded { get; private set; }

    /// <summary>
    /// Gets the reCaptcha response value
    /// </summary>
    public string Response { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the error value
    /// </summary>
    public string Error { get; private set; } = string.Empty;

    private ReCaptchaResult() { }

    /// <summary>
    /// Creates a success result
    /// </summary>
    /// <param name="response">The reCaptcha response value</param>
    /// <returns>The <see cref="ReCaptchaResult"/> instance</returns>
    public static ReCaptchaResult Success(string response)
        => new() { Succeeded = true, Response = response };

    /// <summary>
    /// Creates an error result
    /// </summary>
    /// <param name="error">The error value</param>
    /// <returns>The <see cref="ReCaptchaResult"/> instance</returns>
    public static ReCaptchaResult Failed(string error)
        => new() { Succeeded = false, Error = error };
}
