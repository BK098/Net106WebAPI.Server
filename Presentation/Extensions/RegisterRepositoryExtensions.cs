using Application.Commands.CategoryCommands;
using Application.Commands.ProductCommands;
using Application.Queries.CategoryQueries;
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
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));
            
            //Category
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCategoryCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllCategoriresQuery).Assembly));
            return services;
        }
    }
}
