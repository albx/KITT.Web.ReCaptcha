using KITT.Web.ReCaptcha.Blazor.v3;
using KITT.Web.ReCaptcha.Samples.v3.Blazor.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddReCaptchaV3(options => options.SiteKey = builder.Configuration["ReCaptcha:SiteKey"]!);

builder.Services.AddHttpClient<IReCaptchaFormClient, ReCaptchaFormHttpClient>(
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

await builder.Build().RunAsync();
