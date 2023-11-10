using System.ComponentModel.DataAnnotations;

namespace KITT.Web.ReCaptcha.Sample.Shared;

public class ReCaptchaModel
{
    [Required]
    public string Text { get; set; } = string.Empty;

    [Required]
    public string CaptchaResponse { get; set; } = string.Empty;
}
