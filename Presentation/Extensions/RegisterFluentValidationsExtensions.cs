using Application.Services.Models.CategoryModels;
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

            //Category
            services.AddScoped<IValidator<CategoryForCreate>, CategoryForCreateValidator>();
            return services;
        }
    }
}
