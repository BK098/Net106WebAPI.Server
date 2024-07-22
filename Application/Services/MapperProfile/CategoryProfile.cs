using Application.Services.Models.CategoryModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.MapperProfile
{
    public class CategoryProfile : Profile
    {
        
        public CategoryProfile()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<Category, CategoryForView>()
                .ForMember(c => c.ProductCount, p => p.MapFrom(s => s.Products.Count()));
            CreateMap<CategoryForCreate, Category>();
            CreateMap<CategoryForUpdate, Category>().ForMember(src => src.Id, opt => opt.Ignore());
        }
    }
}
