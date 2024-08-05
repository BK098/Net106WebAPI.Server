using Application.Services.Models.UserModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Text.Json.Serialization;

namespace Application.Queries.UserQueries
{
    public record GetUserProfileQuery(AppUser User) : IRequest<UserForView> { }
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserForView>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AppUser> _logger;
        private readonly IMapper _mapper;
        public GetUserProfileQueryHandler(RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<AppUser> logger,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<UserForView> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByIdAsync(request.User.Id);
            var roles = await _userManager.GetRolesAsync(user);
            UserForView item = _mapper.Map<UserForView>(user);
            item.Role = roles.FirstOrDefault();
            var userForViewList = new List<UserForView>();
            return item;
        }
    }
}
