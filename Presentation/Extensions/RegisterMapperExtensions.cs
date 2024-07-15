using Application.Services.MapperProfile;
using AutoMapper;

namespace Presentation.Extensions
{
    public static class RegisterMapperExtensions
    {
        public static IServiceCollection AddRegistrationMappers(this IServiceCollection services)
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<ProductProfile>();
                c.AddProfile<CategoryProfile>();
                c.AddProfile<ComboProfile>();
                c.AddProfile<AuthenticationProfile>();
                c.AddProfile<UserProfile>();
                c.AddProfile<OrderProfile>();
            });

            services.AddSingleton<IMapper>(s => config.CreateMapper());
        
            return services;
        }
    }
}