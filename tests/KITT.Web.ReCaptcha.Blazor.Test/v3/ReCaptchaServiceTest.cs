using Bunit;
using KITT.Web.ReCaptcha.Blazor.v3;
using Microsoft.JSInterop;

namespace KITT.Web.ReCaptcha.Blazor.Test.v3;

public class ReCaptchaServiceTest : TestContext
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task VerifyAsync_Should_Throw_ArgumentException_If_SiteKey_Is_Empty(string siteKey)
    {
        var service = new ReCaptchaService(JSInterop.JSRuntime);

        var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.VerifyAsync(siteKey, action: "test"));
        Assert.Equal(nameof(siteKey), ex.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task VerifyAsync_Should_Throw_ArgumentException_If_Action_Is_Empty(string action)
    {
        var service = new ReCaptchaService(JSInterop.JSRuntime);

        var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.VerifyAsync(siteKey: "testsitekey", action));
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

        var service = new ReCaptchaService(JSInterop.JSRuntime);
        var result = await service.VerifyAsync(siteKey, action);

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

        var service = new ReCaptchaService(JSInterop.JSRuntime);
        var result = await service.VerifyAsync(siteKey, action);

        Assert.Equal(expectedResult, result);
    }
}
