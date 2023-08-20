using KITT.Web.ReCaptcha.Http.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KITT.Web.ReCaptcha.Http.v2;

/// <summary>
/// Defines the extensions methods to register <see cref="ReCaptchaService"/> in the IoC container
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds <see cref="ReCaptchaService"/> service and configures all the options needed
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance</param>
    /// <param name="configureOptions">The action used to configure the <see cref="ReCaptchaConfiguration"/> options</param>
    /// <returns>The <see cref="IServiceCollection"/> instance for method chaining</returns>
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
