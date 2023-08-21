using System.Text.Json.Serialization;

namespace KITT.Web.ReCaptcha.Http.v2;

/// <summary>
/// Defines the response of the call to the reCaptcha verification endpoint
/// </summary>
public record ReCaptchaResponse
{
    /// <summary>
    /// Gets whether the verification ended successfully
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; init; }

    /// <summary>
    /// Gets the timestamp of the challenge load
    /// </summary>
    [JsonPropertyName("challenge_ts")]
    public DateTime ChallengeTimestamp { get; init; }

    /// <summary>
    /// Gets the hostname of the site where the reCAPTCHA was solved
    /// </summary>
    [JsonPropertyName("hostname")]
    public string Hostname { get; init; } = string.Empty;

    /// <summary>
    /// Gets the optional list of error codes (see https://developers.google.com/recaptcha/docs/verify#error_code_reference)
    /// </summary>
    [JsonPropertyName("error-codes")]
    public IEnumerable<string> ErrorCodes { get; init; } = Enumerable.Empty<string>();
}
