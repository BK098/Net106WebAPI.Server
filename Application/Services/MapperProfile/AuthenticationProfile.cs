using Application.Services.Models.AuthenticationModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.MapperProfile
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            Init();
        }
        private void Init()
        {
            CreateMap<RegisterModel, AppUser>();
        }
    }
}
