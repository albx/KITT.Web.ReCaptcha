using KITT.Web.ReCaptcha.Http.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KITT.Web.ReCaptcha.Http.v2;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReCaptchaV2(
        this IServiceCollection services,
        Action<ReCaptchaConfiguration> configureOptions)
    {
        services.Configure(configureOptions);

        services.AddHttpClient();

        services.AddHttpClient<ReCaptchaService>(
            client => client.BaseAddress = new Uri("https://www.google.com/recaptcha/"));

        return services;
    }
}
