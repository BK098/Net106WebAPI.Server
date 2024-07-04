using Application.Services.Contracts.Services.Base;
using Application.Services.Contracts.Services.Commands;
using Application.Services.Localizations;
using Application.Services.Services.Commands;

namespace Presentation.Extensions
{
    public static class RegisterServicesExtensions
    {
        public static IServiceCollection AddRegistrationServices(this IServiceCollection services)
        {
            services.AddScoped<ILocalizationMessage, LocalizationMessage>();
            services.AddScoped<IProductCommandService, ProductCommandService>();
            return services;
        }
    }
}
