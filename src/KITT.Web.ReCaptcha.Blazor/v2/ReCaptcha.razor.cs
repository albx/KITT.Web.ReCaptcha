using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace KITT.Web.ReCaptcha.Blazor.v2;

public partial class ReCaptcha : IAsyncDisposable
{
    private IJSObjectReference? module;

    private static readonly string elementId = "recaptcha";

    [Inject]
    public IJSRuntime Js { get; set; } = default!;

    [Parameter]
    [EditorRequired]
    public string SiteKey { get; set; } = string.Empty;

    [Parameter]
    public string Id { get; set; } = elementId;

    [Parameter]
    public int TabIndex { get; set; } = 0;

    [Parameter]
    public Theme Theme { get; set; } = Theme.Light;

    [Parameter]
    public Size Size { get; set; } = Size.Normal;

    protected override void OnInitialized()
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            Id = elementId;
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

    [JSInvokable]
    public void Success(string response)
    {
        CurrentValue = response;
    }

    [JSInvokable]
    public void Expired()
    {
        CurrentValue = string.Empty;
    }

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
