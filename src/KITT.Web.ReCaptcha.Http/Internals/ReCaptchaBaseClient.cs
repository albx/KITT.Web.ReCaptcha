using KITT.Web.ReCaptcha.Http.Configuration;

namespace KITT.Web.ReCaptcha.Http.Internals;

/// <summary>
/// Defines the base class for reCaptcha server-side validation
/// </summary>
public abstract class ReCaptchaBaseClient
{
    /// <summary>
    /// The <see cref="HttpClient"/> instance
    /// </summary>
    protected readonly HttpClient _httpClient;

    /// <summary>
    /// The <see cref="ReCaptchaConfiguration"/> instance
    /// </summary>
    protected readonly ReCaptchaConfiguration _configuration;

    /// <summary>
    /// Constructs the client instance
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> instance configured to call the Google API</param>
    /// <param name="configuration">The <see cref="ReCaptchaConfiguration"/> instance configured with the secret key</param>
    /// <exception cref="ArgumentNullException">Thrown when <see cref="HttpClient"/> or <see cref="ReCaptchaConfiguration"/> instance is null</exception>
    protected ReCaptchaBaseClient(HttpClient httpClient, ReCaptchaConfiguration? configuration)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        ThrowIfConfigurationIsNotValid(_configuration);
    }

    /// <summary>
    /// Builds and sends the Http request for the reCaptcha server-side validation
    /// </summary>
    /// <param name="response">The user response token provided by the reCAPTCHA client-side integration on your site.</param>
    /// <param name="remoteIp">The user's IP address.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> instance</param>
    /// <returns>The <see cref="HttpResponseMessage"/> returned from the HTTP request</returns>
    protected async Task<HttpResponseMessage> SendReCaptchaRequestAsync(string response, string? remoteIp = null, CancellationToken cancellationToken = default)
    {
        var content = new FormUrlEncodedContent(new Dictionary<string, string?>
        {
            ["secret"] = _configuration.SecretKey,
            ["response"] = response,
            ["remoteip"] = remoteIp
        });

        var reCaptchaResponse = await _httpClient.PostAsync("api/siteverify", content, cancellationToken);
        reCaptchaResponse.EnsureSuccessStatusCode();

        return reCaptchaResponse;
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
