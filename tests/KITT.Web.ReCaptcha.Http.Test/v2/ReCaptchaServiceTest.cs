using KITT.Web.ReCaptcha.Http.Configuration;
using KITT.Web.ReCaptcha.Http.v2;
using Microsoft.Extensions.Options;
using RichardSzalay.MockHttp;
using System.Text.Json;

namespace KITT.Web.ReCaptcha.Http.Test.v2;

public class ReCaptchaServiceTest
{
    private static readonly Uri _googleRecaptchaBaseUri = new Uri("https://www.google.com/recaptcha/");

    #region Ctor tests
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Ctor_Should_Throw_Argument_Exception_If_Secret_Key_Is_Missing(string secretKey)
    {
        using HttpClient httpClient = new HttpClient();
        IOptions<ReCaptchaConfiguration> reCaptchaConfigurationOptions = Options.Create(new ReCaptchaConfiguration { SecretKey = secretKey });

        var ex = Assert.Throws<ArgumentException>(() => new ReCaptchaService(httpClient, reCaptchaConfigurationOptions));
        Assert.Equal(nameof(ReCaptchaConfiguration.SecretKey), ex.ParamName);
    }
    #endregion

    #region VerifyAsync tests
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task VerifyAsync_Should_Throw_ArgumentException_If_Response_Is_Empty(string response)
    {
        using var httpClient = new HttpClient();
        var reCaptchaConfigurationOptions = Options.Create(new ReCaptchaConfiguration { SecretKey = "fakesecretkey" });

        var service = new ReCaptchaService(httpClient, reCaptchaConfigurationOptions);

        var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.VerifyAsync(response));
        Assert.Equal(nameof(response), ex.ParamName);
    }

    [Fact]
    public async Task VerifyAsync_Should_Return_Missing_Input_Secret_Error_If_Secret_Is_Not_Valid()
    {
        using var httpClientMock = new MockHttpMessageHandler();
        httpClientMock.When(HttpMethod.Post, "https://www.google.com/recaptcha/api/siteverify")
            .Respond("application/json", JsonSerializer.Serialize(new Dictionary<string, object>
            {
                ["sucess"] = false,
                ["challenge_ts"] = DateTime.MinValue,
                ["error-codes"] = new[] { "missing-input-secret" }
            }));

        using var httpClient = httpClientMock.ToHttpClient();
        httpClient.BaseAddress = _googleRecaptchaBaseUri;

        var reCaptchaConfigurationOptions = Options.Create(new ReCaptchaConfiguration { SecretKey = "fakesecretkey" });

        var service = new ReCaptchaService(httpClient, reCaptchaConfigurationOptions);
        var response = await service.VerifyAsync("fakeresponse");

        Assert.False(response.Success);
        Assert.Contains("missing-input-secret", response.ErrorCodes);
    }

    [Fact]
    public async Task VerifyAsync_Should_Return_Invalid_Input_Secret_Error_If_Secret_Is_Not_Valid()
    {
        using var httpClientMock = new MockHttpMessageHandler();
        httpClientMock.When(HttpMethod.Post, "https://www.google.com/recaptcha/api/siteverify")
            .Respond("application/json", JsonSerializer.Serialize(new Dictionary<string, object>
            {
                ["sucess"] = false,
                ["challenge_ts"] = DateTime.MinValue,
                ["error-codes"] = new[] { "invalid-input-secret" }
            }));

        using var httpClient = httpClientMock.ToHttpClient();
        httpClient.BaseAddress = _googleRecaptchaBaseUri;

        var reCaptchaConfigurationOptions = Options.Create(new ReCaptchaConfiguration { SecretKey = "fakesecretkey" });

        var service = new ReCaptchaService(httpClient, reCaptchaConfigurationOptions);
        var response = await service.VerifyAsync("fakeresponse");

        Assert.False(response.Success);
        Assert.Contains("invalid-input-secret", response.ErrorCodes);
    }

    [Fact]
    public async Task VerifyAsync_Should_Return_Missing_Input_Response_Error_If_Response_Is_Missing()
    {
        using var httpClientMock = new MockHttpMessageHandler();
        httpClientMock.When(HttpMethod.Post, "https://www.google.com/recaptcha/api/siteverify")
            .Respond("application/json", JsonSerializer.Serialize(new Dictionary<string, object>
            {
                ["sucess"] = false,
                ["challenge_ts"] = DateTime.MinValue,
                ["error-codes"] = new[] { "missing-input-response" }
            }));

        using var httpClient = httpClientMock.ToHttpClient();
        httpClient.BaseAddress = _googleRecaptchaBaseUri;

        var reCaptchaConfigurationOptions = Options.Create(new ReCaptchaConfiguration { SecretKey = "fakesecretkey" });

        var service = new ReCaptchaService(httpClient, reCaptchaConfigurationOptions);
        var response = await service.VerifyAsync("fakeresponse");

        Assert.False(response.Success);
        Assert.Contains("missing-input-response", response.ErrorCodes);
    }

    [Fact]
    public async Task VerifyAsync_Should_Return_Invalid_Input_Response_Error_If_Response_Is_Not_Valid()
    {
        using var httpClientMock = new MockHttpMessageHandler();
        httpClientMock.When(HttpMethod.Post, "https://www.google.com/recaptcha/api/siteverify")
            .Respond("application/json", JsonSerializer.Serialize(new Dictionary<string, object>
            {
                ["sucess"] = false,
                ["challenge_ts"] = DateTime.MinValue,
                ["error-codes"] = new[] { "invalid-input-response" }
            }));

        using var httpClient = httpClientMock.ToHttpClient();
        httpClient.BaseAddress = _googleRecaptchaBaseUri;

        var reCaptchaConfigurationOptions = Options.Create(new ReCaptchaConfiguration { SecretKey = "fakesecretkey" });

        var service = new ReCaptchaService(httpClient, reCaptchaConfigurationOptions);
        var response = await service.VerifyAsync("fakeresponse");

        Assert.False(response.Success);
        Assert.Contains("invalid-input-response", response.ErrorCodes);
    }

    [Fact]
    public async Task VerifyAsync_Should_Return_Timeout_Or_Duplicate_Error_If_Response_Is_No_Longer_Valid_Or_Has_Been_Already_Used()
    {
        using var httpClientMock = new MockHttpMessageHandler();
        httpClientMock.When(HttpMethod.Post, "https://www.google.com/recaptcha/api/siteverify")
            .Respond("application/json", JsonSerializer.Serialize(new Dictionary<string, object>
            {
                ["sucess"] = false,
                ["challenge_ts"] = DateTime.MinValue,
                ["error-codes"] = new[] { "timeout-or-duplicate" }
            }));

        using var httpClient = httpClientMock.ToHttpClient();
        httpClient.BaseAddress = _googleRecaptchaBaseUri;

        var reCaptchaConfigurationOptions = Options.Create(new ReCaptchaConfiguration { SecretKey = "fakesecretkey" });

        var service = new ReCaptchaService(httpClient, reCaptchaConfigurationOptions);
        var response = await service.VerifyAsync("fakeresponse");

        Assert.False(response.Success);
        Assert.Contains("timeout-or-duplicate", response.ErrorCodes);
    }
    #endregion
}
