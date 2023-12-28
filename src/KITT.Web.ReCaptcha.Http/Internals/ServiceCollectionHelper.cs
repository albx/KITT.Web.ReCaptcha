using KITT.Web.ReCaptcha.Http.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KITT.Web.ReCaptcha.Http.Internals;

internal static class ServiceCollectionHelper
{
    public static IHttpClientBuilder RegisterClient<TClient>(IServiceCollection services, Action<ReCaptchaConfiguration> configureOptions)
        where TClient : ReCaptchaBaseClient
    {
        services.Configure(configureOptions);

        services.AddHttpClient();

        var builder = services.AddHttpClient<TClient>(
            client => client.BaseAddress = new Uri("https://www.google.com/recaptcha/"));

        return builder;
    }
}
