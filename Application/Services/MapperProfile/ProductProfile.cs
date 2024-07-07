﻿using Application.Services.Models.ProductModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.MapperProfile
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            Init();
        }
        private void Init()
        {
            CreateMap<ProductForCreate, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<Product, ProductForView>();
            CreateMap<Product, ProductForViewItems>();
            CreateMap<ProductForUpdate, Product>().ForMember(src => src.Id, opt => opt.Ignore());
        }
    }
}
