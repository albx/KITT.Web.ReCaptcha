using KITT.Web.ReCaptcha.Samples.v2.Blazor.Components;
using KITT.Web.ReCaptcha.Http.v2;
using KITT.Web.ReCaptcha.Sample.Shared;
using Microsoft.AspNetCore.Mvc;
using KITT.Web.ReCaptcha.Samples.v2.Blazor.Client.Services;
using KITT.Web.ReCaptcha.Samples.v2.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

//The secret key is the testing secret key provided by google for reCaptcha v2
builder.Services.AddReCaptchaV2HttpClient(options => options.SecretKey = "6LeIxAcTAAAAAGG-vFI1TnRWxMZNFuojJ4WifJWe");

builder.Services.AddScoped<IReCaptchaFormClient, ReCaptchaFormClient>();

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
    .AddAdditionalAssemblies(typeof(KITT.Web.ReCaptcha.Samples.v2.Blazor.Client._Imports).Assembly);

app.MapPost("/api/send", async (ReCaptchaService reCaptcha, [FromBody] ReCaptchaModel model) =>
{
    var result = await reCaptcha.VerifyAsync(model.CaptchaResponse);

    if (!result.Success)
    {
        return Results.BadRequest(result.ErrorCodes);
    }

    return Results.Ok();
});

app.Run();
