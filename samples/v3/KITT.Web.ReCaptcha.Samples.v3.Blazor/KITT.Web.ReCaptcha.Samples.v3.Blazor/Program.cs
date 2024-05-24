using KITT.Web.ReCaptcha.Samples.v3.Blazor.Components;
using KITT.Web.ReCaptcha.Blazor.v3;
using KITT.Web.ReCaptcha.Http.v3;
using KITT.Web.ReCaptcha.Sample.Shared;
using Microsoft.AspNetCore.Mvc;
using KITT.Web.ReCaptcha.Samples.v3.Blazor.Client.Services;
using KITT.Web.ReCaptcha.Samples.v3.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReCaptchaV3(options => options.SiteKey = builder.Configuration["ReCaptcha:SiteKey"]!);
builder.Services.AddReCaptchaV3HttpClient(options => options.SecretKey = builder.Configuration["ReCaptcha:SecretKey"]!);

builder.Services.AddScoped<IReCaptchaFormClient, ReCaptchaFormClient>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(KITT.Web.ReCaptcha.Samples.v3.Blazor.Client._Imports).Assembly);

app.MapPost("/api/send", async (KITT.Web.ReCaptcha.Http.v3.ReCaptchaService reCaptcha, [FromBody] ReCaptchaModel model) =>
{
    var result = await reCaptcha.VerifyAsync(model.CaptchaResponse, action: "submit");

    if (!result.Success)
    {
        return Results.BadRequest(result.ErrorCodes);
    }

    return Results.Ok();
});

app.Run();
