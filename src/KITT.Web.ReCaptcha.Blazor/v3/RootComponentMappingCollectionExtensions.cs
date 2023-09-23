using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace KITT.Web.ReCaptcha.Blazor.v3;

public static class RootComponentMappingCollectionExtensions
{
    public static void RegisterReCaptchaScript(this RootComponentMappingCollection rootComponents)
        => rootComponents.Add<ReCaptchaScript>("body::after");
}
