using KITT.Web.ReCaptcha.Http.v2;
using KITT.Web.ReCaptcha.Sample.Shared;
using KITT.Web.ReCaptcha.Samples.v2.Blazor.Client.Services;

namespace KITT.Web.ReCaptcha.Samples.v2.Blazor.Services;

public class ReCaptchaFormClient(ReCaptchaService reCaptcha) : IReCaptchaFormClient
{
    public ReCaptchaService ReCaptcha { get; } = reCaptcha;

    public async Task<ReCaptchaFormResult> SendAsync(ReCaptchaModel model)
    {
        var result = await ReCaptcha.VerifyAsync(model.CaptchaResponse);

        if (result.Success)
        {
            return ReCaptchaFormResult.Succeeded;
        }

        return ReCaptchaFormResult.Failed(
            "Validation failed!",
            result.ErrorCodes.ToArray());
    }
}
