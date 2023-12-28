using KITT.Web.ReCaptcha.Http.v3;
using Microsoft.Extensions.DependencyInjection;

namespace KITT.Web.ReCaptcha.Http.Test.v3;

public class ServiceCollectionTest
{
    [Fact]
    public void AddReCaptchaV3HttpClient_Should_Add_ReCaptchaService_To_Service_Descriptors()
    {
        var services = new ServiceCollection();

        ServiceCollectionExtensions.AddReCaptchaV3HttpClient(
            services,
            options => options.SecretKey = "secretKey");

        Assert.Contains(
            services,
            serviceDescriptor => serviceDescriptor.ServiceType == typeof(ReCaptchaService));
    }
}
