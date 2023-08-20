# KITT.Web.ReCaptcha.Http

This project add Google reCaptcha to your ASP.NET Core apps giving the service to validate your reCaptcha client response.<br/>
This project targets **.NET 6** and **.NET 7** as supported Framework versions.

## Installation

This project is available on NuGet.

It can be installed using the ```dotnet add package``` command or the NuGet wizard on your favourite IDE.

```bash
  dotnet add package KITT.Web.ReCaptcha.Http
```

## Usage

The project gives you an HttpClient service which expose a *VerifyAsync* method to verify the reCaptcha response send by the user from the client.

Add the namespace ```KITT.Web.ReCaptcha.Http.v2``` to your ```Program.cs``` and use the *AddReCaptchaV2* extension method to your ```IServiceCollection``` instance:

```
builder.Services.AddReCaptchaV2(options =>
{
    options.SecretKey = "<your reCaptcha server-side secret key>";
});
```

Then you can inject the ```ReCaptchaService``` class whenever you need and call the ```VerifyAsync``` like this:

```
app.MapPost("/send", async (ReCaptchaService reCaptchaService, [FromBody] SendRequest request) =>
{
    // Here you call the reCaptcha server-side validation
    var captchaResponse = await reCaptchaService.VerifyAsync(request.CaptchaResponse);
    if (!captchaResponse.Success)
    {
        return Results.BadRequest(captchaResponse.ErrorCodes);
    }

    return Results.Ok();
});
```

## Methods

The ```VerifyAsync``` has the following input parameters:

|Property|Description|
|---|---|
|**response** (Required)|*string*: The user response token provided by the reCAPTCHA client-side integration on your site.|
|**remoteIp** (Optional)|*string*: The user's IP address. (Default: *null*)|
|**cancellationToken** (Optional)|*CancellationToken*: a cancellation token instance (Default: *CancellationToken.None*)|

The response of the method is mapped by the ```ReCaptchaResponse``` class:

|Property|Description|
|---|---|
|**Success**|*bool*: whether the verification ended successfully|
|**ChallengeTimestamp**|*DateTime*: the timestamp of the challenge load|
|**Hostname**|*string*: the hostname of the site where the reCAPTCHA was solved|
|**ErrorCodes**|*IEnumerable&lt;string&gt;*: the optional list of error codes (see [Google's official documentation](https://developers.google.com/recaptcha/docs/verify#error_code_reference))|