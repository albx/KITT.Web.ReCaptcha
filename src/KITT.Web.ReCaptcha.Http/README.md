# KITT.Web.ReCaptcha.Http

This project add Google reCaptcha to your ASP.NET Core apps giving the service to validate your reCaptcha client response.<br/>
This project targets **.NET 6** and **.NET 9** as supported Framework versions.

## Installation

This project is available on NuGet.

It can be installed using the `dotnet add package` command or the NuGet wizard on your favourite IDE.

```bash
  dotnet add package KITT.Web.ReCaptcha.Http
```

## reCaptcha v2

### Usage

The project gives you an HttpClient service which expose a _VerifyAsync_ method to verify the reCaptcha response send by the user from the client.

Add the namespace `KITT.Web.ReCaptcha.Http.v2` to your `Program.cs` and use the _AddReCaptchaV2HttpClient_ extension method to your `IServiceCollection` instance:

```
builder.Services.AddReCaptchaV2HttpClient(options =>
{
    options.SecretKey = "<your reCaptcha server-side secret key>";
});
```

Then you can inject the `ReCaptchaService` class whenever you need and call the `VerifyAsync` like this:

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

### Methods

The `VerifyAsync` method has the following input parameters:

| Property                         | Description                                                                                       |
| -------------------------------- | ------------------------------------------------------------------------------------------------- |
| **response** (Required)          | _string_: The user response token provided by the reCAPTCHA client-side integration on your site. |
| **remoteIp** (Optional)          | _string_: The user's IP address. (Default: _null_)                                                |
| **cancellationToken** (Optional) | _CancellationToken_: a cancellation token instance (Default: _CancellationToken.None_)            |

The method returns an instance of the `ReCaptchaResponse` class, which have the following properties:

| Property               | Description                                                                                                                                                                     |
| ---------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Success**            | _bool_: whether the verification ended successfully                                                                                                                             |
| **ChallengeTimestamp** | _DateTime_: the timestamp of the challenge load                                                                                                                                 |
| **Hostname**           | _string_: the hostname of the site where the reCAPTCHA was solved                                                                                                               |
| **ErrorCodes**         | _IEnumerable&lt;string&gt;_: the optional list of error codes (see [Google's official documentation](https://developers.google.com/recaptcha/docs/verify#error_code_reference)) |

## reCaptcha v3

### Usage

The project gives you an HttpClient service which expose a _VerifyAsync_ method to verify the reCaptcha response send by the user from the client.

Add the namespace `KITT.Web.ReCaptcha.Http.v3` to your `Program.cs` and use the _AddReCaptchaV3HttpClient_ extension method to your `IServiceCollection` instance:

```
builder.Services.AddReCaptchaV3HttpClient(options =>
{
    options.SecretKey = "<your reCaptcha server-side secret key>";
});
```

Then you can inject the `ReCaptchaService` class whenever you need and call the `VerifyAsync` like this:

```
app.MapPost("/send", async (ReCaptchaService reCaptchaService, [FromBody] SendRequest request) =>
{
    // Here you call the reCaptcha server-side validation
    var captchaResponse = await reCaptchaService.VerifyAsync(request.CaptchaResponse, request.Action);
    if (!captchaResponse.Success)
    {
        return Results.BadRequest(captchaResponse.ErrorCodes);
    }

    return Results.Ok();
});
```

### Methods

The `VerifyAsync` method has the following input parameters:

| Property                         | Description                                                                                       |
| -------------------------------- | ------------------------------------------------------------------------------------------------- |
| **response** (Required)          | _string_: The user response token provided by the reCAPTCHA client-side integration on your site. |
| **action** (Required)            | _string_: The action value used to configure the reCaptcha                                        |
| **remoteIp** (Optional)          | _string_: The user's IP address. (Default: _null_)                                                |
| **cancellationToken** (Optional) | _CancellationToken_: a cancellation token instance (Default: _CancellationToken.None_)            |

The method returns an instance of the `ReCaptchaResponse` class, which have the following properties:

| Property               | Description                                                                                                                                                                     |
| ---------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Success**            | _bool_: whether the verification ended successfully                                                                                                                             |
| **Score**              | _double_: the score for the request (from 0.0 to 1.0)                                                                                                                           |
| **Action**             | _string_: the action name for this request                                                                                                                                      |
| **ChallengeTimestamp** | _DateTime_: the timestamp of the challenge load                                                                                                                                 |
| **Hostname**           | _string_: the hostname of the site where the reCAPTCHA was solved                                                                                                               |
| **ErrorCodes**         | _IEnumerable&lt;string&gt;_: the optional list of error codes (see [Google's official documentation](https://developers.google.com/recaptcha/docs/verify#error_code_reference)) |
