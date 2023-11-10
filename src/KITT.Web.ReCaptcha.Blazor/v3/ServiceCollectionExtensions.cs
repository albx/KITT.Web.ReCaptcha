using Microsoft.Extensions.DependencyInjection;

namespace KITT.Web.ReCaptcha.Blazor.v3;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all the dependencies to the <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance</param>
    /// <param name="configureOptions">The method to configure the <see cref="ReCaptchaConfiguration"/></param>
    /// <returns>The <see cref="IServiceCollection"/> instance for method chaining</returns>
    public static IServiceCollection AddReCaptchaV3(this IServiceCollection services, Action<ReCaptchaConfiguration> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(configureOptions);

        services.Configure(configureOptions);
        services.AddScoped<ReCaptchaService>();

        return services;
    }
}
