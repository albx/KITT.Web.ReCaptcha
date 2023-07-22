using KITT.Web.ReCaptcha.Http.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;

namespace KITT.Web.ReCaptcha.Http.v2;

public class ReCaptchaService
{
    private readonly HttpClient _httpClient;

    private readonly ReCaptchaConfiguration _configuration;

    public ReCaptchaService(HttpClient httpClient, IOptions<ReCaptchaConfiguration> reCaptchaConfigurationOptions)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _configuration = reCaptchaConfigurationOptions?.Value ?? throw new ArgumentNullException();
    }

    public async Task<ReCaptchaResponse> VerifyAsync(string response, string? remoteIp = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(response))
        {
            throw new ArgumentException("value cannot be empty", nameof(response));
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

        return responseResult!;
    }
}
