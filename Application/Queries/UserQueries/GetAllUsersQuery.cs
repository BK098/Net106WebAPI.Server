using Application.Services.Models.UserModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Queries.UserQueries
{
    public class GetAllUsersQuery : UserForView, IRequest<UserForView>
    {
    }
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, UserForView>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AppUser> _logger;
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandler(RoleManager<IdentityRole> roleManager,
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
        public async Task<UserForView> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<AppUser> users = await _userManager.Users.ToListAsync();
                IList<UserForViewItems> items = _mapper.Map<IEnumerable<UserForViewItems>>(users).ToList();
                UserForView responsse = new UserForView();
                responsse.Users = items;
                return responsse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}
