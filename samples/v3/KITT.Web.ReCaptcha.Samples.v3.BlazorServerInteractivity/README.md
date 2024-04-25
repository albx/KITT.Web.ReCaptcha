# KITT.Web.ReCaptcha - Blazor Web App with global Server interactivity

This is a sample using Blazor Web App with global Server interactivity.

## Usage
Create a reCaptcha v3 configuration in your reCaptcha [Admin Console](https://www.google.com/recaptcha/admin/create) (see https://developers.google.com/recaptcha/docs/v3 for further informations).

Once you have created the configuration, take the Site Key and put in the appsettings.json file in this section:

```json
"ReCaptcha": {
  "SiteKey": "<put your recaptcha site key here>"
}
```

Then you can run the application and submit the form you'll find in the Home page to see reCaptcha in action.