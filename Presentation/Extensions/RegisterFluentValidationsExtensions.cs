using Application.Services.Models.CategoryModels;
using Application.Services.Models.ComboModels;
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
            services.AddScoped<IValidator<ProductForUpdate>, ProductForUpdateValidator>();
            //Category
            services.AddScoped<IValidator<CategoryForCreate>, CategoryForCreateValidator>();
            services.AddScoped<IValidator<CategoryForUpdate>, CategoryForUpdateValidator>();
            //Combo
            services.AddScoped<IValidator<ComboForCreate>, ComboForCreateValidator>();
            services.AddScoped<IValidator<ProductComboInforCreate>, ProductComboInforCreateValidator>();
            services.AddScoped<IValidator<ComboForUpdate>, ComboForUpdateValidator>();
            services.AddScoped<IValidator<ProductItemInfoForUpdate>, ProductItemInfoForUpdateValidator>();
            return services;
        }
    }
}