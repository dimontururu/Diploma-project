using Application.Interfaces;
using Application.Interfaces.Localizer;
using Infrastructure.Api;
using Infrastructure.Localization;
using Infrastructure.service;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Configure
    {
        public static IServiceCollection InfrastructureADD(this IServiceCollection ser)
        {
            return ser.AddSingleton<ISessionService,SessionService>()
                .AddSingleton<ILanguageSelector,LanguageSelector>()
                .AddScoped<ITokenRefresher, TokenRefresher>()
                .AddScoped<IApiRetryHelper, ApiRetryHelper>();
        }
    }
}
