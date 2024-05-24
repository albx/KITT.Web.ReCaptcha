using KITT.Web.ReCaptcha.Http.v3;
using KITT.Web.ReCaptcha.Sample.Shared;
using KITT.Web.ReCaptcha.Samples.v3.Blazor.Client.Services;

namespace KITT.Web.ReCaptcha.Samples.v3.Blazor.Services;

public class ReCaptchaFormClient(ReCaptchaService reCaptcha) : IReCaptchaFormClient
{
    public ReCaptchaService ReCaptcha { get; } = reCaptcha;

    public async Task<ReCaptchaFormResult> SendAsync(ReCaptchaModel model)
    {
        var result = await ReCaptcha.VerifyAsync(model.CaptchaResponse, action: "submit");

        if (result.Success)
        {
            return ReCaptchaFormResult.Succeeded;
        }

        return ReCaptchaFormResult.Failed(
            "Validation failed!",
            result.ErrorCodes.ToArray());
    }
}
