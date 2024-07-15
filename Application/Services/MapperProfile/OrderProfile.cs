using Application.Services.Models.OrderModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.MapperProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            Init();
        }
        private void Init()
        {
            //view
            CreateMap<Order, OrderForView>();
            CreateMap<Order, OrderForViewItems>()
                 //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : string.Empty))
                 //.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User != null ? src.User.Id : string.Empty))
                 .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<OrderItem, OrderItemForView>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
                //.ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                //.ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product.Image));
            //create
            CreateMap<OrderForCreate, Order>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());

            CreateMap<OrderItemForCreate, OrderItem>()
                .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore());

            //update
            /*CreateMap<OrderForUpdate, Order>();*/
            CreateMap<OrderForUpdate, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());
                /*CreateMap<OrderDetailForUpdate, OrderDetail>()
                    .ForMember(src => src.OrderId, opt => opt.Ignore())
                    .ForMember(dest => dest.Order, opt => opt.Ignore());*/

        }
    }
}
