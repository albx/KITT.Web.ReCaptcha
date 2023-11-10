using Bunit;
using KITT.Web.ReCaptcha.Blazor.v3;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace KITT.Web.ReCaptcha.Blazor.Test.v3;

public class ReCaptchaServiceTest : TestContext
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Ctor_Should_Throw_ArgumentException_If_Site_Key_Is_Empty(string siteKey)
    {
        var reCaptchaConfigurationOptions = Options.Create(new ReCaptchaConfiguration { SiteKey = siteKey });

        var ex = Assert.Throws<ArgumentException>(() => new ReCaptchaService(JSInterop.JSRuntime, reCaptchaConfigurationOptions));
        Assert.Equal(nameof(ReCaptchaConfiguration.SiteKey), ex.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task VerifyAsync_Should_Throw_ArgumentException_If_Action_Is_Empty(string action)
    {
        var reCaptchaConfigurationOptions = Options.Create(new ReCaptchaConfiguration { SiteKey = "testsitekey" });
        var service = new ReCaptchaService(JSInterop.JSRuntime, reCaptchaConfigurationOptions);

        var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.VerifyAsync(action));
        Assert.Equal(nameof(action), ex.ParamName);
    }

    [Fact]
    public async Task VerifyAsync_Should_Return_Failed_Result_If_Site_Key_Is_Not_Valid()
    {
        var siteKey = "invalidsitekey";
        var action = "testaction";

        var errorMessage = "invalid site key";
        var expectedResult = ReCaptchaResult.Failed(errorMessage);

        var module = JSInterop.SetupModule("./_content/KITT.Web.ReCaptcha.Blazor/v3/recaptcha-v3.js");
        module.Setup<string>("execute", siteKey, action).SetException(new JSException(errorMessage));

        var reCaptchaConfigurationOptions = Options.Create(new ReCaptchaConfiguration { SiteKey = siteKey });

        var service = new ReCaptchaService(JSInterop.JSRuntime, reCaptchaConfigurationOptions);
        var result = await service.VerifyAsync(action);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public async Task VerifyAsync_Should_Return_Success_Result_If_Everything_Is_Ok()
    {
        var siteKey = "testsitekey";
        var action = "testaction";

        var expectedResponse = Guid.NewGuid().ToString();
        var expectedResult = ReCaptchaResult.Success(expectedResponse);

        var module = JSInterop.SetupModule("./_content/KITT.Web.ReCaptcha.Blazor/v3/recaptcha-v3.js");
        module.Setup<string>("execute", siteKey, action).SetResult(expectedResponse);

        var reCaptchaConfigurationOptions = Options.Create(new ReCaptchaConfiguration { SiteKey = siteKey });

        var service = new ReCaptchaService(JSInterop.JSRuntime, reCaptchaConfigurationOptions);
        var result = await service.VerifyAsync(action);

        Assert.Equal(expectedResult, result);
    }
}
