using System.Text.Json.Serialization;

namespace KITT.Web.ReCaptcha.Http.v3;

public record ReCaptchaResponse
{
    /// <summary>
    /// Gets whether the verification ended successfully
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; init; }

    [JsonPropertyName("score")]
    public double Score { get; init; }

    [JsonPropertyName("action")]
    public string Action { get; init; } = string.Empty;

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
