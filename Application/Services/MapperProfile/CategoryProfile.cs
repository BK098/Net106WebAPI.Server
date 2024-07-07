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
            CreateMap<Category, CategoryForView>();
            CreateMap<Category, CategoryForViewItems>();
            CreateMap<CategoryForCreate, Category>();
            CreateMap<CategoryForUpdate, Category>().ForMember(src => src.Id, opt => opt.Ignore());
        }
    }
}
