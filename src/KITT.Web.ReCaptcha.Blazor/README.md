# KITT.Web.ReCaptcha.Blazor

This project add Google reCaptcha to your Blazor apps.<br/>
This project targets **.NET 7** as supported Framework version.

## Installation

This project is available on [NuGet](https://www.nuget.org/packages/KITT.Web.ReCaptcha.Blazor).

It can be installed using the ```dotnet add package``` command or the NuGet wizard on your favourite IDE.

```bash
  dotnet add package KITT.Web.ReCaptcha.Blazor
```

## reCaptcha v2
### Usage

The project gives you a Razor component which add the reCaptcha v2 widget.

Add the namespace ```KITT.Web.ReCaptcha.Blazor.v2``` to your Razor Components or in the ```_Imports.razor``` file.

After that you can use the ```<ReCaptcha />``` component in your ```EditForm``` like in this sample:

```csharp
  <ReCaptcha SiteKey="<your reCaptcha v2 client key>"
             @bind-Value="yourPropertyToBind"
             Theme="Theme.Dark"
             Size="Size.Normal"
             TabIndex="1" />

  @code {
    private string yourPropertyToBind = string.Empty;
  }
```

### Parameters

The component needs to be used inside an ```EditForm```. It exposes the following properties:

|Property|Description|
|---|---|
|**SiteKey** (Required)|*string*: the value of the v2 client site key|
|**@bind-Value**|*string*: the property to bind|
|**Theme**|*Theme* enum: the theme to use (default: *Theme.Light*)|
|**Size**|*Size* enum: the size of the widget (default: *Size.Normal*)|
|**TabIndex**|*int*: the tabIndex value (default: *0*)|
|**Id**|*string*: the id of the HTML element used to render the reCaptcha widget (*default*: "recaptcha")|

## reCaptcha v3
### Usage

You can register reCaptcha using the ```AddReCaptchaV3``` extension method:
```csharp
  builder.Services.AddReCaptchaV3(options => options.SiteKey = "<YOUR CLIENT SITE KEY VALUE>");
```
After that you have to add the *ReCaptchaScript* component. If you're using Blazor WebAssembly, simply register it to the RootComponents calling the extension method:
```csharp
  builder.RootComponents.RegisterReCaptchaScript();
```

If you're using Blazor Server, edit the *_Host.cshtml* page in this way:
```razor
  ....
  <script src="https://www.google.com/recaptcha/api.js" async defer></script>
  <script src="_framework/blazor.server.js"></script>
  <component type="typeof(ReCaptchaScript)" render-mode="ServerPrerendered" />
```
After that you can inject the ReCaptchaService to your component and call the VerifyAsync method:
```razor
  @inject ReCaptchaService ReCaptcha
  ...
  @code {
    private async Task SubmitAsync(){
      var result = await ReCaptcha.VerifyAsync(action: "submit");
      // ....
    }
  }
```

## Samples

In the [samples](https://github.com/albx/KITT.Web.ReCaptcha/tree/main/samples) folder you can find both Blazor WebAssembly and Blazor Server samples.
The site key used in this samples is only for testing purpose.
