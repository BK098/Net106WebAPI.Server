using Application.Services.Contracts.Repositories;
using Repositories.Repositories;

namespace Presentation.Extensions
{
    public static class RegisterRepositoryExtensions
    {
        public static IServiceCollection AddRegistrationRepositories(this IServiceCollection services)
        {
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IComboRepository, ComboRepository>();
            return services;
        }
    }
}
