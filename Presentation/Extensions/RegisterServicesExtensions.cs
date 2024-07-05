using Application.Services.Contracts.Services.Base;
using Application.Services.Localizations;
using Application.Services.Services;
using System.Reflection;

namespace Presentation.Extensions
{
    public static class RegisterServicesExtensions
    {
        public static IServiceCollection AddRegistrationServices(this IServiceCollection services)
        {
            services.AddScoped<ILocalizationMessage, LocalizationMessage>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).GetTypeInfo().Assembly));

            return services;
        }
    }
}