using KITT.Web.ReCaptcha.Blazor.v3;
using KITT.Web.ReCaptcha.Sample.v3.Wasm;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.RootComponents.RegisterReCaptchaScript();

builder.Services.AddReCaptchaV3(options =>
{
    options.SiteKey = builder.Configuration["ReCaptcha:SiteKey"]!;
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
