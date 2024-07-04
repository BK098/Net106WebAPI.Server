using Application.Services.Models.ProductModels;
using FluentValidation;

namespace Presentation.Extensions
{
    public static class RegisterFluentValidationsExtensions
    {
        public static IServiceCollection AddRegistrationFluentValidations(this IServiceCollection services)
        {
            //Product
            services.AddScoped<IValidator<ProductForCreate>, ProductForCreateValidator>();

            return services;
        }
    }
}
