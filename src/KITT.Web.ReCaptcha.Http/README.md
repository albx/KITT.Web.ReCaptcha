# KITT.Web.ReCaptcha.Http

This project add Google reCaptcha to your ASP.NET Core apps giving the service to validate your reCaptcha client response.<br/>
This project targets **.NET 6** and **.NET 8** as supported Framework versions.

## Installation

This project is available on NuGet.

It can be installed using the ```dotnet add package``` command or the NuGet wizard on your favourite IDE.

```bash
  dotnet add package KITT.Web.ReCaptcha.Http
```

## reCaptcha v2
### Usage

The project gives you an HttpClient service which expose a *VerifyAsync* method to verify the reCaptcha response send by the user from the client.

Add the namespace ```KITT.Web.ReCaptcha.Http.v2``` to your ```Program.cs``` and use the *AddReCaptchaV2HttpClient* extension method to your ```IServiceCollection``` instance:

```
builder.Services.AddReCaptchaV2HttpClient(options =>
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

### Methods

The ```VerifyAsync``` method has the following input parameters:

|Property|Description|
|---|---|
|**response** (Required)|*string*: The user response token provided by the reCAPTCHA client-side integration on your site.|
|**remoteIp** (Optional)|*string*: The user's IP address. (Default: *null*)|
|**cancellationToken** (Optional)|*CancellationToken*: a cancellation token instance (Default: *CancellationToken.None*)|

The method returns an instance of the ```ReCaptchaResponse``` class, which have the following properties:

|Property|Description|
|---|---|
|**Success**|*bool*: whether the verification ended successfully|
|**ChallengeTimestamp**|*DateTime*: the timestamp of the challenge load|
|**Hostname**|*string*: the hostname of the site where the reCAPTCHA was solved|
|**ErrorCodes**|*IEnumerable&lt;string&gt;*: the optional list of error codes (see [Google's official documentation](https://developers.google.com/recaptcha/docs/verify#error_code_reference))|

## reCaptcha v3
### Usage

The project gives you an HttpClient service which expose a *VerifyAsync* method to verify the reCaptcha response send by the user from the client.

Add the namespace ```KITT.Web.ReCaptcha.Http.v3``` to your ```Program.cs``` and use the *AddReCaptchaV3HttpClient* extension method to your ```IServiceCollection``` instance:

```
builder.Services.AddReCaptchaV3HttpClient(options =>
{
    options.SecretKey = "<your reCaptcha server-side secret key>";
});
```

Then you can inject the ```ReCaptchaService``` class whenever you need and call the ```VerifyAsync``` like this:

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

The ```VerifyAsync``` method has the following input parameters:

|Property|Description|
|---|---|
|**response** (Required)|*string*: The user response token provided by the reCAPTCHA client-side integration on your site.|
|**action** (Required)|*string*: The action value used to configure the reCaptcha|
|**remoteIp** (Optional)|*string*: The user's IP address. (Default: *null*)|
|**cancellationToken** (Optional)|*CancellationToken*: a cancellation token instance (Default: *CancellationToken.None*)|

The method returns an instance of the ```ReCaptchaResponse``` class, which have the following properties:

|Property|Description|
|---|---|
|**Success**|*bool*: whether the verification ended successfully|
|**Score**|*double*: the score for the request (from 0.0 to 1.0)|
|**Action**|*string*: the action name for this request|
|**ChallengeTimestamp**|*DateTime*: the timestamp of the challenge load|
|**Hostname**|*string*: the hostname of the site where the reCAPTCHA was solved|
|**ErrorCodes**|*IEnumerable&lt;string&gt;*: the optional list of error codes (see [Google's official documentation](https://developers.google.com/recaptcha/docs/verify#error_code_reference))|

