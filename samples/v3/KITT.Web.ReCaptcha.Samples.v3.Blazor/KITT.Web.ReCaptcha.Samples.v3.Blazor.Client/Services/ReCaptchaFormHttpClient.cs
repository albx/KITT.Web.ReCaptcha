using KITT.Web.ReCaptcha.Sample.Shared;
using System.Net.Http.Json;

namespace KITT.Web.ReCaptcha.Samples.v3.Blazor.Client.Services;

public class ReCaptchaFormHttpClient(HttpClient http) : IReCaptchaFormClient
{
    public HttpClient Http { get; } = http;

    public async Task<ReCaptchaFormResult> SendAsync(ReCaptchaModel model)
    {
        var response = await Http.PostAsJsonAsync("api/send", model);
        if (!response.IsSuccessStatusCode)
        {
            var errorCodes = await response.Content.ReadFromJsonAsync<string[]>() ?? [];
            return ReCaptchaFormResult.Failed("Http Validation failed", errorCodes);
        }

        return ReCaptchaFormResult.Succeeded;
    }
}
