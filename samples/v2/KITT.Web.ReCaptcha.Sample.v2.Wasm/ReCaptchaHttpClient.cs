using KITT.Web.ReCaptcha.Sample.Shared;
using System.Net.Http.Json;

namespace KITT.Web.ReCaptcha.Sample.v2.Wasm;

public class ReCaptchaHttpClient
{
    public HttpClient Http { get; }

    public ReCaptchaHttpClient(HttpClient http)
    {
        Http = http ?? throw new ArgumentNullException(nameof(http));
    }

    public async Task<VerifyResponse> VerifyAsync(ReCaptchaModel model)
    {
        var response = await Http.PostAsJsonAsync("api/verify", model);
        if (response.IsSuccessStatusCode)
        {
            return VerifyResponse.Success;
        }

        var errorCodes = await response.Content.ReadFromJsonAsync<string[]>() ?? Enumerable.Empty<string>();
        return VerifyResponse.Failed(errorCodes);
    }
}
