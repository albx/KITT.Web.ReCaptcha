using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace KITT.Web.ReCaptcha.Blazor.v3;

/// <summary>
/// Extension methods for the <see cref="RootComponentMappingCollection"/>
/// </summary>
public static class RootComponentMappingCollectionExtensions
{
    /// <summary>
    /// Registers the <see cref="ReCaptchaScript"/> component to the DOM
    /// </summary>
    /// <param name="rootComponents">The <see cref="RootComponentMappingCollection"/> instance</param>
    public static void RegisterReCaptchaScript(this RootComponentMappingCollection rootComponents)
        => rootComponents.Add<ReCaptchaScript>("body::after");
}
