using Application.Commands.ProductCommands;
using Application.Queries.ProductQueries;
using Application.Services.Contracts.Services.Base;
using Application.Services.Contracts.Services.Commands;
using Application.Services.Contracts.Services.Queries;
using Application.Services.Localizations;
using Application.Services.Services.Commands;
using Application.Services.Services.Queries;

namespace Presentation.Extensions
{
    public static class RegisterServicesExtensions
    {
        public static IServiceCollection AddRegistrationServices(this IServiceCollection services)
        {
            services.AddScoped<ILocalizationMessage, LocalizationMessage>();
            services.AddScoped<IProductCommandService, ProductCommandService>();
            services.AddScoped<IProductQueryService, ProductQueryService>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllProductsQuery).Assembly));

            return services;
        }
    }
}
