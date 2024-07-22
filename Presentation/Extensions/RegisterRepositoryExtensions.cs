using Application.Services.Contracts.Repositories;
using Application.Services.Contracts.Repositories.Base;
using Repositories.Repositories;
using Repositories.Repositories.Base;

namespace Presentation.Extensions
{
    public static class RegisterRepositoryExtensions
    {
        public static IServiceCollection AddRegistrationRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IComboRepository, ComboRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
