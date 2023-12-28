using KITT.Web.ReCaptcha.Http.Configuration;
using KITT.Web.ReCaptcha.Http.Internals;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace KITT.Web.ReCaptcha.Http.v2;

/// <summary>
/// This service verifies the captcha response from the client calling the Google API
/// </summary>
public class ReCaptchaService : ReCaptchaBaseClient
{
    /// <summary>
    /// Constructs the service instance
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> instance configured to call the Google API</param>
    /// <param name="reCaptchaConfigurationOptions">The <see cref="IOptions{ReCaptchaConfiguration}"/> instance which contains the server side secret key</param>
    /// <exception cref="ArgumentNullException">Thrown when <see cref="HttpClient"/> or <see cref="IOptions{ReCaptchaConfiguration}"/> instance is null</exception>
    public ReCaptchaService(HttpClient httpClient, IOptions<ReCaptchaConfiguration> reCaptchaConfigurationOptions)
        : base(httpClient, reCaptchaConfigurationOptions?.Value)
    {
    }

    /// <summary>
    /// Verifies the response of the reCaptcha client side integration
    /// </summary>
    /// <param name="response">(Required) The user response token provided by the reCAPTCHA client-side integration on your site.</param>
    /// <param name="remoteIp">(Optional) The user's IP address.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> instance</param>
    /// <returns>The <see cref="ReCaptchaResponse"/> received from the call to the Google verification endpoint</returns>
    /// <exception cref="ArgumentException">Thrown when response is null or white-space</exception>
    public async Task<ReCaptchaResponse> VerifyAsync(string response, string? remoteIp = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(response))
        {
            throw new ArgumentException("value cannot be empty", nameof(response));
        }

        var reCaptchaResponse = await SendReCaptchaRequestAsync(response, remoteIp, cancellationToken).ConfigureAwait(false);

        var responseResult = await reCaptchaResponse.Content.ReadFromJsonAsync<ReCaptchaResponse>(
            cancellationToken: cancellationToken);

        return responseResult!;
    }
}
