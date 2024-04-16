using KITT.Web.ReCaptcha.Sample.Shared;

namespace KITT.Web.ReCaptcha.Samples.v3.Blazor.Client.Services;

public interface IReCaptchaFormClient
{
    Task<ReCaptchaFormResult> SendAsync(ReCaptchaModel model);
}
