using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace KITT.Web.ReCaptcha.Blazor.v2;

/// <summary>
/// The v2 ReCaptcha component
/// </summary>
public partial class ReCaptcha : IAsyncDisposable
{
    private IJSObjectReference? module;

    private const string ElementId = "recaptcha";

    /// <summary>
    /// Gets or sets the injected <see cref="IJSRuntime"/> instance
    /// </summary>
    [Inject]
    public IJSRuntime Js { get; set; } = default!;

    /// <summary>
    /// Gets or sets the Google reCaptcha v2 client site key
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string SiteKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the id of the HTML element used to render the reCaptcha widget
    /// </summary>
    [Parameter]
    public string Id { get; set; } = ElementId;

    /// <summary>
    /// Gets or sets the tabIndex property value
    /// </summary>
    [Parameter]
    public int TabIndex { get; set; } = 0;

    /// <summary>
    /// Gets or sets the theme property value (<see cref="v2.Theme"/>)
    /// </summary>
    [Parameter]
    public Theme Theme { get; set; } = Theme.Light;

    /// <summary>
    /// Gets or sets the size property value (<see cref="v2.Size"/>)
    /// </summary>
    [Parameter]
    public Size Size { get; set; } = Size.Normal;

    /// <summary>
    /// Gets or sets the callback invoked on reCaptcha expiration
    /// </summary>
    [Parameter]
    public EventCallback OnExpired { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked on reCaptcha error
    /// </summary>
    [Parameter]
    public EventCallback OnError { get; set; }

    protected override void OnInitialized()
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            Id = ElementId;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            module = await Js.InvokeAsync<IJSObjectReference>(
                "import",
                "./_content/KITT.Web.ReCaptcha.Blazor/v2/ReCaptcha.razor.js");

            await module.InvokeAsync<int>(
                "initialize",
                DotNetObjectReference.Create(this),
                Id,
                SiteKey,
                TabIndex,
                Theme.ToString().ToLower(),
                Size.ToString().ToLower());
        }
    }

    #region JSInvokable methods
    /// <summary>
    /// The method invoked on reCaptcha success operation
    /// </summary>
    /// <param name="response">The reCaptcha response</param>
    [JSInvokable]
    public void Success(string response)
    {
        CurrentValue = response;
    }

    /// <summary>
    /// The method invoked on reCaptcha expiration
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task Expired()
    {
        CurrentValue = string.Empty;
        await OnExpired.InvokeAsync();
    }

    /// <summary>
    /// The method invoked on reCaptcha error
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task Error()
    {
        CurrentValue = string.Empty;
        await OnError.InvokeAsync();
    }
    #endregion

    /// <see cref="IAsyncDisposable.DisposeAsync"/>
    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (module is not null)
        {
            await module.DisposeAsync();
        }
    }

    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out string result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        validationErrorMessage = string.Empty;
        result = value ?? string.Empty;

        return true;
    }
}
