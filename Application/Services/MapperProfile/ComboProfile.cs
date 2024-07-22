using Application.Services.Models.ComboModels;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Services.MapperProfile
{
    public class ComboProfile : Profile
    {
        public ComboProfile()
        {
            Init();
        }
        public void Init()
        {
            CreateMap<Combo, ComboForView>()
             .ForMember(c => c.ProductCombos, p => p.MapFrom(s => s.ProductCombos));
            CreateMap<ProductCombo, ProductComboForView>()
                .ForMember(c => c.ProductImage, p => p.MapFrom(s => s.Product.Image))
                .ForMember(c => c.ProductName, p => p.MapFrom(s => s.Product.Name))
                .ForMember(c => c.ProductPrice, p => p.MapFrom(s => s.Product.Price))
                .ForMember(c => c.ProductId, p => p.MapFrom(s => s.Product.Id));
            //Create
            CreateMap<ComboForCreate, Combo>()
                .ForMember(src => src.Id, opt => opt.MapFrom(x => Guid.NewGuid()))
                .ForMember(src => src.ProductCombos, opt => opt.Ignore());

            CreateMap<ProductComboForCreate, ProductCombo>()
                .ForMember(src => src.ComboId, opt => opt.Ignore())
                .ForMember(src => src.Combo, opt => opt.Ignore());

            //Update
            CreateMap<ComboForUpdate, Combo>()
                .ForMember(src => src.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductCombos, opt => opt.Ignore());

            CreateMap<ProductComboForUpdate, ProductCombo>()
                .ForMember(src => src.ComboId, opt => opt.Ignore())
                .ForMember(src => src.Combo, opt => opt.Ignore());
        }
    }
}
