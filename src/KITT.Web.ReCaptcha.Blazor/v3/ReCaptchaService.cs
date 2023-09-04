using Microsoft.JSInterop;

namespace KITT.Web.ReCaptcha.Blazor.v3;

public class ReCaptchaService : IAsyncDisposable
{
    private readonly IJSRuntime _jSRuntime;

    private IJSObjectReference? _module;

    public ReCaptchaService(IJSRuntime jSRuntime)
    {
        _jSRuntime = jSRuntime ?? throw new ArgumentNullException(nameof(jSRuntime));
    }

    public async Task<ReCaptchaResult> VerifyAsync(string siteKey, string action)
    {
        if (string.IsNullOrWhiteSpace(siteKey))
        {
            throw new ArgumentException("value cannot be empty", nameof(siteKey));
        }

        if (string.IsNullOrWhiteSpace(action))
        {
            throw new ArgumentException("value cannot be empty", nameof(action));
        }

        try
        {
            await LoadReCaptchaModuleAsync();
            if (_module is null)
            {
                throw new InvalidOperationException("reCaptcha not loaded");
            }

            var response = await _module.InvokeAsync<string>("execute", siteKey, action);
            return ReCaptchaResult.Success(response);
        }
        catch (JSException ex)
        {
            return ReCaptchaResult.Failed(ex.Message);
        }
    }

    private async Task LoadReCaptchaModuleAsync()
    {
        if (_module is null)
        {
            _module = await _jSRuntime.InvokeAsync<IJSObjectReference>(
                "import",
                "./_content/KITT.Web.ReCaptcha.Blazor/v3/recaptcha-v3.js");
        }
    }

    #region AsyncDisposable
    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (_module is not null)
        {
            await _module.DisposeAsync();
        }
    }
    #endregion
}
