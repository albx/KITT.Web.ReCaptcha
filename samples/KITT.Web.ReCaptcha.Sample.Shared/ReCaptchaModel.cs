using System.ComponentModel.DataAnnotations;

namespace KITT.Web.ReCaptcha.Sample.Shared;

public class ReCaptchaModel : IValidatableObject
{
    [Required]
    public string Text { get; set; } = string.Empty;

    public string CaptchaResponse { get; set; } = string.Empty;

    private bool _captchaFromUi;

    public ReCaptchaModel(bool captchaFromUi = false)
    {
        _captchaFromUi = captchaFromUi;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var result = new List<ValidationResult>();

        if (_captchaFromUi && string.IsNullOrWhiteSpace(CaptchaResponse))
        {
            result.Add(new ValidationResult("Captcha is required", [nameof(CaptchaResponse)]));
        }

        return result;
    }
}
