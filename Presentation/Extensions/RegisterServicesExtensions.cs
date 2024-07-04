using Application.Services.Contracts.Services.Base;
using Application.Services.Localizations;

namespace Presentation.Extensions
{
    public static class RegisterServicesExtensions
    {
        public static IServiceCollection AddRegistrationServices(this IServiceCollection services)
        {
            services.AddScoped<ILocalizationMessageError, LocalizationMessageError>();

            return services;
        }
    }
}
