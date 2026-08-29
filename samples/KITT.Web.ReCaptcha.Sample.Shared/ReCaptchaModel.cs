using System.ComponentModel.DataAnnotations;

namespace KITT.Web.ReCaptcha.Sample.Shared;

public class ReCaptchaModel
{
    [Required]
    public string Text { get; set; } = string.Empty;

    public string CaptchaResponse { get; set; } = string.Empty;
}
