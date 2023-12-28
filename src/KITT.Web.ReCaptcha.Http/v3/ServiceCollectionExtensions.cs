using KITT.Web.ReCaptcha.Http.Configuration;
using KITT.Web.ReCaptcha.Http.Internals;
using Microsoft.Extensions.DependencyInjection;

namespace KITT.Web.ReCaptcha.Http.v3;

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
    /// <returns>The <see cref="IHttpClientBuilder"/> instance for method chaining</returns>
    public static IHttpClientBuilder AddReCaptchaV3HttpClient(
        this IServiceCollection services,
        Action<ReCaptchaConfiguration> configureOptions)
    {
        return ServiceCollectionHelper.RegisterClient<ReCaptchaService>(services, configureOptions);
    }
}
