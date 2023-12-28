using KITT.Web.ReCaptcha.Http.v2;
using Microsoft.Extensions.DependencyInjection;

namespace KITT.Web.ReCaptcha.Http.Test.v2;

public class ServiceCollectionTest
{
    [Fact]
    public void AddReCaptchaV2HttpClient_Should_Add_ReCaptchaService_To_Service_Descriptors()
    {
        var services = new ServiceCollection();

        ServiceCollectionExtensions.AddReCaptchaV2HttpClient(
            services,
            options => options.SecretKey = "secretKey");

        Assert.Contains(
            services,
            serviceDescriptor => serviceDescriptor.ServiceType == typeof(ReCaptchaService));
    }
}
