
using Application.Commands.UserCommands;
using Application.Services.Models.UserModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.MapperProfile
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<AppUser, UserForView>();
            CreateMap<AppUser, UserForViewItems>();
            CreateMap<UserProfileForUpdate, AppUser>()
           .ForMember(dest => dest.UserName, opt => opt.Ignore())
           .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
           .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore());
        }
    }
}
