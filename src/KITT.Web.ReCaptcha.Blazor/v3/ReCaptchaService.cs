using Microsoft.JSInterop;

namespace KITT.Web.ReCaptcha.Blazor.v3;

/// <summary>
/// This service performs the client-side validation for Google reCaptcha v3
/// </summary>
public class ReCaptchaService : IAsyncDisposable
{
    private readonly IJSRuntime _jSRuntime;

    private IJSObjectReference? _module;

    /// <summary>
    /// Constructs the service instance
    /// </summary>
    /// <param name="jSRuntime">The <see cref="IJSRuntime"/> instance</param>
    /// <exception cref="ArgumentNullException">Thrown if the <see cref="IJSRuntime"/> is null</exception>
    public ReCaptchaService(IJSRuntime jSRuntime)
    {
        _jSRuntime = jSRuntime ?? throw new ArgumentNullException(nameof(jSRuntime));
    }

    /// <summary>
    /// Verifies the reCaptcha v3
    /// </summary>
    /// <param name="siteKey">The reCaptcha client-side v3 site key</param>
    /// <param name="action">The action to specify in the reCaptcha</param>
    /// <returns>A <see cref="ReCaptchaResult"/> instance</returns>
    /// <exception cref="ArgumentException">Site key or action are empty</exception>
    /// <exception cref="InvalidOperationException">When the reCaptcha module is not loaded correctly</exception>
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
