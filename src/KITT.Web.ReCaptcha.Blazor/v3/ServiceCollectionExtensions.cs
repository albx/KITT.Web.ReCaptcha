using Microsoft.Extensions.DependencyInjection;

namespace KITT.Web.ReCaptcha.Blazor.v3;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReCaptchaV3(this IServiceCollection services, Action<ReCaptchaConfiguration> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(configureOptions);

        services.Configure(configureOptions);
        return services;
    }
}
