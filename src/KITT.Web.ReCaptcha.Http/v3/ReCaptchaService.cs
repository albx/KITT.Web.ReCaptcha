using KITT.Web.ReCaptcha.Http.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace KITT.Web.ReCaptcha.Http.v3;

public class ReCaptchaService
{
    private readonly HttpClient _httpClient;

    private readonly ReCaptchaConfiguration _configuration;

    public ReCaptchaService(HttpClient httpClient, IOptions<ReCaptchaConfiguration> reCaptchaConfigurationOptions)
    {
        _httpClient = httpClient;
        _configuration = reCaptchaConfigurationOptions?.Value ?? throw new ArgumentNullException(nameof(reCaptchaConfigurationOptions));

        ThrowIfConfigurationIsNotValid(_configuration);
    }

    public async Task<ReCaptchaResponse> VerifyAsync(string response, string action, string? remoteIp = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(response))
        {
            throw new ArgumentException("value cannot be empty", nameof(response));
        }

        if (string.IsNullOrWhiteSpace(action))
        {
            throw new ArgumentException("value cannot be empty", nameof(action));
        }

        var content = new FormUrlEncodedContent(new Dictionary<string, string?>
        {
            ["secret"] = _configuration.SecretKey,
            ["response"] = response,
            ["remoteip"] = remoteIp
        });

        var reCaptchaResponse = await _httpClient.PostAsync("api/siteverify", content, cancellationToken);
        reCaptchaResponse.EnsureSuccessStatusCode();

        var responseResult = await reCaptchaResponse.Content.ReadFromJsonAsync<ReCaptchaResponse>(
            cancellationToken: cancellationToken);

        if (responseResult!.Action != action)
        {
            throw new InvalidOperationException("action values does not match");
        }

        return responseResult!;
    }

    #region Private methods
    private static void ThrowIfConfigurationIsNotValid(ReCaptchaConfiguration configuration)
    {
        if (string.IsNullOrWhiteSpace(configuration.SecretKey))
        {
            throw new ArgumentException("value cannot be empty", nameof(configuration.SecretKey));
        }
    }
    #endregion
}
