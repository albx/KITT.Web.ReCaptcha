using System.Text.Json.Serialization;

namespace KITT.Web.ReCaptcha.Http.v2;

public record ReCaptchaResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; init; }

    [JsonPropertyName("challenge_ts")]
    public DateTime ChallengeTimestamp { get; init; }

    [JsonPropertyName("hostname")]
    public string Hostname { get; init; } = string.Empty;

    [JsonPropertyName("error-codes")]
    public IEnumerable<string> ErrorCodes { get; init; } = Enumerable.Empty<string>();
}
